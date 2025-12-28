using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestoranOtomasyonu.WebAPI.Data;
using RestoranOtomasyonu.WebAPI.Models;

namespace RestoranOtomasyonu.WebAPI.Services
{
    public class OpenAiSettings
    {
        public string ApiKey { get; set; } = string.Empty;
        // DeepSeek model adı (ör: deepseek-reasoner, deepseek-chat)
        public string Model { get; set; } = "deepseek-reasoner";
        // DeepSeek API base URL
        public string BaseUrl { get; set; } = "https://api.deepseek.com/v1";
        public string RestaurantName { get; set; } = "Restoranınız";
        public bool IncludeDebugMenuContext { get; set; } = false;
    }

    public class ChatBotService : IChatBotService
    {
        private readonly RestaurantAiDbContext _db;
        private readonly HttpClient _httpClient;
        private readonly OpenAiSettings _settings;
        private readonly ILogger<ChatBotService> _logger;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
        
        // Together.ai API request için özel serializer (JsonPropertyName attribute'ları kullanılacak)
        private static readonly JsonSerializerOptions RequestJsonOptions = new()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        // Together.ai API request DTO (snake_case property names)
        private class TogetherApiRequest
        {
            [JsonPropertyName("model")]
            public string Model { get; set; } = string.Empty;
            
            [JsonPropertyName("messages")]
            public List<TogetherApiMessage> Messages { get; set; } = new();
            
            [JsonPropertyName("temperature")]
            public double? Temperature { get; set; }
            
            [JsonPropertyName("max_tokens")]
            public int? MaxTokens { get; set; }
        }
        
        private class TogetherApiMessage
        {
            [JsonPropertyName("role")]
            public string Role { get; set; } = string.Empty;
            
            [JsonPropertyName("content")]
            public string Content { get; set; } = string.Empty;
        }

        public ChatBotService(
            RestaurantAiDbContext db,
            HttpClient httpClient,
            IOptions<OpenAiSettings> options,
            ILogger<ChatBotService> logger)
        {
            _db = db;
            _httpClient = httpClient;
            _settings = options.Value;
            _logger = logger;
        }

        public async Task<ChatResponseDto> GetAiResponseAsync(ChatRequestDto request, CancellationToken cancellationToken = default)
        {
            // Request ve mesaj kontrolü
            if (request == null)
            {
                _logger.LogError("ChatRequestDto null geldi.");
                return new ChatResponseDto
                {
                    Reply = "Üzgünüm, mesajınızı işleyemedim. Lütfen tekrar deneyin.",
                    FromLiveSupport = false
                };
            }

            if (string.IsNullOrWhiteSpace(request.Message))
            {
                _logger.LogError("Kullanıcı mesajı boş geldi.");
                return new ChatResponseDto
                {
                    Reply = "Lütfen bir mesaj yazın.",
                    FromLiveSupport = false
                };
            }

            // API Key kontrolü
            if (string.IsNullOrWhiteSpace(_settings.ApiKey))
            {
                _logger.LogError("DeepSeek API anahtarı (ApiKey) yapılandırılmamış. appsettings.json dosyasında 'OpenAI:ApiKey' ayarını kontrol edin.");
                return new ChatResponseDto
                {
                    Reply = "Üzgünüm, şu anda sohbet hizmeti yapılandırılmamış. Lütfen daha sonra tekrar deneyin.",
                    FromLiveSupport = false
                };
            }

            // Kullanıcı mesajını logla (debug için)
            _logger.LogInformation("ChatBotService: Kullanıcı mesajı alındı. Message: '{Message}' (Length: {Length}), Model: {Model}", 
                request.Message, request.Message?.Length ?? 0, _settings.Model);

            var menuItems = await GetActiveMenuItemsAsync(cancellationToken);
            var menuJson = JsonSerializer.Serialize(menuItems, JsonOptions);
            var systemPrompt = BuildSystemPrompt(menuJson);

            // OpenRouter API (OpenAI-compatible) chat completions isteği
            // POST https://openrouter.ai/api/v1/chat/completions
            var requestUrl = $"{_settings.BaseUrl}/chat/completions";

            // Together.ai API format (snake_case property names)
            var body = new TogetherApiRequest
            {
                Model = _settings.Model,
                Messages = new List<TogetherApiMessage>
                {
                    new TogetherApiMessage { Role = "system", Content = systemPrompt },
                    new TogetherApiMessage { Role = "user", Content = request.Message }
                },
                Temperature = 0.4,
                MaxTokens = 400
            };

            // JSON serialization - OpenRouter API snake_case bekliyor
            string jsonBody;
            try
            {
                // Request body için snake_case kullan (max_tokens, not maxTokens)
                jsonBody = JsonSerializer.Serialize(body, RequestJsonOptions);
                _logger.LogInformation("DeepSeek request body serialized. Length: {Length}, Preview: {Preview}", 
                    jsonBody.Length,
                    jsonBody.Length > 500 ? jsonBody.Substring(0, 500) + "..." : jsonBody);
            }
            catch (Exception jsonEx)
            {
                _logger.LogError(jsonEx, "JSON serialization hatası. Request body oluşturulamadı.");
                return new ChatResponseDto
                {
                    Reply = "Üzgünüm, mesajınız işlenirken bir hata oluştu. Lütfen tekrar deneyin.",
                    FromLiveSupport = false
                };
            }

            var jsonContent = new StringContent(
                jsonBody,
                Encoding.UTF8,
                "application/json");

            // OpenRouter API için Authorization header ekle
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_settings.ApiKey}");
            // OpenRouter için HTTP Referer header'ı ekle (opsiyonel ama önerilir)
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://github.com/yourusername/restoran-otomasyonu");
            _httpClient.DefaultRequestHeaders.Add("X-Title", "Restoran Otomasyonu Chatbot");
            
            // API key ve URL'i logla (güvenlik için sadece ilk ve son karakterler)
            var maskedApiKey = _settings.ApiKey.Length > 10 
                ? $"{_settings.ApiKey.Substring(0, 4)}...{_settings.ApiKey.Substring(_settings.ApiKey.Length - 4)}" 
                : "***";
            _logger.LogInformation("🔑 OpenRouter API Key (masked): {MaskedKey}, URL: {Url}, Model: {Model}", 
                maskedApiKey, requestUrl, _settings.Model);

            try
            {
                _logger.LogInformation("DeepSeek API'ye istek gönderiliyor. URL: {Url}, Model: {Model}, Message Length: {MessageLength}", 
                    requestUrl, 
                    _settings.Model,
                    request.Message?.Length ?? 0);
                
                using var response = await _httpClient.PostAsync(requestUrl, jsonContent, cancellationToken);
                _logger.LogInformation("DeepSeek API yanıtı alındı. StatusCode: {StatusCode}, ReasonPhrase: {ReasonPhrase}", 
                    response.StatusCode, response.ReasonPhrase);

                if (!response.IsSuccessStatusCode)
                {
                    var errorText = await response.Content.ReadAsStringAsync(cancellationToken);

                    _logger.LogError("DeepSeek HTTP Hatası: {StatusCode} - {ReasonPhrase} | Body: {Body} | Request Message: {RequestMessage}",
                        (int)response.StatusCode,
                        response.ReasonPhrase,
                        errorText,
                        request.Message);

                    // Kullanıcıya anlaşılır mesaj döndür
                    string userMessage = "Üzgünüm, şu anda yanıt veremiyorum. Lütfen biraz sonra tekrar deneyin.";
                    
                    // Rate limit hatası - özel mesaj
                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        userMessage = "Üzgünüm, şu anda çok fazla istek alındığı için sohbet servisi geçici olarak kullanılamıyor. Lütfen birkaç dakika sonra tekrar deneyin.";
                        _logger.LogWarning("⚠️ DeepSeek API rate limit'e takıldı. Kullanıcıya bilgilendirme mesajı gönderildi.");
                    }
                    // Eğer API key hatası ise özel mesaj
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || 
                        response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        userMessage = "Üzgünüm, sohbet servisi şu anda kullanılamıyor. Lütfen daha sonra tekrar deneyin.";
                        _logger.LogError("❌ DeepSeek API anahtarı geçersiz veya yetkisiz erişim. StatusCode: {StatusCode}, Error Response: {ErrorResponse}, API Key (masked): {MaskedKey}", 
                            response.StatusCode, 
                            errorText,
                            _settings.ApiKey.Length > 10 
                                ? $"{_settings.ApiKey.Substring(0, 4)}...{_settings.ApiKey.Substring(_settings.ApiKey.Length - 4)}" 
                                : "***");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        _logger.LogError("DeepSeek API'ye gönderilen istek geçersiz. Request body kontrol edilmeli.");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        userMessage = "Üzgünüm, sohbet servisi şu anda yapılandırma hatası nedeniyle kullanılamıyor. Lütfen daha sonra tekrar deneyin.";
                        _logger.LogError("DeepSeek API model bulunamadı (404). Model adı veya API versiyonu yanlış olabilir.");
                    }

                    return new ChatResponseDto
                    {
                        Reply = userMessage,
                        FromLiveSupport = false,
                        DebugMenuContext = _settings.IncludeDebugMenuContext ? menuJson : null
                    };
                }

                // Response içeriğini string olarak al (debug için)
                var responseText = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogInformation("DeepSeek API yanıtı alındı. Length: {Length}, Preview: {Preview}", 
                    responseText.Length,
                    responseText.Length > 1000 ? responseText.Substring(0, 1000) + "..." : responseText);

                using var doc = JsonDocument.Parse(responseText);

                // DeepSeek API (OpenAI-compatible) response: choices[0].message.content
                string content = string.Empty;
                try
                {
                    var root = doc.RootElement;
                    
                    // Önce response yapısını logla
                    var rootProperties = string.Join(", ", root.EnumerateObject().Select(p => p.Name));
                    _logger.LogInformation("🔍 DeepSeek response root element properties: {Properties}", rootProperties);

                    // Error kontrolü önce
                    if (root.TryGetProperty("error", out var errorElement))
                    {
                        var errorJson = errorElement.GetRawText();
                        _logger.LogError("❌ DeepSeek API error response: {Error}", errorJson);
                        content = "Üzgünüm, sohbet servisi şu anda bir hata aldı. Lütfen daha sonra tekrar deneyin.";
                    }
                    else if (root.TryGetProperty("choices", out var choices))
                    {
                        _logger.LogInformation("✅ Choices found. Type: {Type}, Length: {Length}", 
                            choices.ValueKind, 
                            choices.ValueKind == JsonValueKind.Array ? choices.GetArrayLength() : -1);

                        if (choices.ValueKind == JsonValueKind.Array && choices.GetArrayLength() > 0)
                        {
                            var firstChoice = choices[0];
                            var choiceProperties = string.Join(", ", firstChoice.EnumerateObject().Select(p => p.Name));
                            _logger.LogInformation("✅ First choice properties: {Properties}", choiceProperties);

                            // finish_reason kontrolü - eğer blocked ise
                            if (firstChoice.TryGetProperty("finish_reason", out var finishReason))
                            {
                                var reason = finishReason.GetString();
                                _logger.LogInformation("Finish reason: {Reason}", reason);
                                if (reason == "content_filter" || reason == "length")
                                {
                                    _logger.LogWarning("⚠️ DeepSeek yanıtı engellendi veya kesildi. Reason: {Reason}", reason);
                                    if (reason == "content_filter")
                                        content = "Üzgünüm, mesajınız güvenlik kontrolünden geçemedi. Lütfen farklı bir soru deneyin.";
                                }
                            }

                            // DeepSeek OpenAI-compatible format: choices[0].message.content
                            if (firstChoice.TryGetProperty("message", out var messageElement))
                            {
                                var messageProps = string.Join(", ", messageElement.EnumerateObject().Select(p => p.Name));
                                _logger.LogInformation("✅ Message element found. Properties: {Properties}", messageProps);

                                if (messageElement.TryGetProperty("content", out var contentElement))
                                {
                                    content = contentElement.GetString() ?? string.Empty;
                                    _logger.LogInformation("✅✅✅ DeepSeek yanıtı başarıyla parse edildi! Content Length: {Length}, Preview: {Preview}", 
                                        content.Length,
                                        content.Length > 100 ? content.Substring(0, 100) + "..." : content);
                                }
                                else
                                {
                                    _logger.LogError("❌ Message element'te 'content' property bulunamadı! Message JSON: {MessageJson}", 
                                        messageElement.GetRawText());
                                }
                            }
                            else
                            {
                                _logger.LogError("❌ First choice'ta 'message' property bulunamadı! Choice JSON: {ChoiceJson}", 
                                    firstChoice.GetRawText());
                            }
                        }
                        else
                        {
                            _logger.LogError("❌ Choices array boş veya geçersiz. Type: {Type}, Length: {Length}", 
                                choices.ValueKind,
                                choices.ValueKind == JsonValueKind.Array ? choices.GetArrayLength() : -1);
                        }
                    }
                    else
                    {
                        _logger.LogError("❌ Response'ta 'choices' property bulunamadı! Root properties: {Properties}, Full Response: {Response}", 
                            rootProperties,
                            responseText);
                    }
                }
                catch (Exception parseEx)
                {
                    _logger.LogError(parseEx, "❌❌❌ DeepSeek cevabı parse edilirken hata oluştu! Exception: {ExceptionType}, Message: {Message}, Response: {Response}", 
                        parseEx.GetType().Name,
                        parseEx.Message,
                        responseText);
                    content = "Üzgünüm, yanıt işlenirken bir sorun oluştu. Lütfen tekrar deneyin.";
                }

                // Eğer content boşsa, varsayılan mesaj döndür
                if (string.IsNullOrWhiteSpace(content))
                {
                    _logger.LogError("❌ DeepSeek'ten boş yanıt alındı! Request: '{RequestMessage}'. Full Response: {Response}", 
                        request.Message,
                        responseText);
                    
                    // Alternatif parse denemesi - belki farklı bir yapı var
                    try
                    {
                        var root = doc.RootElement;
                        // Error kontrolü
                        if (root.TryGetProperty("error", out var errorElement))
                        {
                            var errorJson = errorElement.GetRawText();
                            _logger.LogError("DeepSeek API error response: {Error}", errorJson);
                            content = "Üzgünüm, sohbet servisi şu anda bir hata aldı. Lütfen daha sonra tekrar deneyin.";
                        }
                        else
                        {
                            // Tüm response yapısını logla
                            _logger.LogError("DeepSeek response parse edilemedi. Root properties: {Properties}, Full JSON: {Json}", 
                                string.Join(", ", root.EnumerateObject().Select(p => p.Name)),
                                responseText);
                            content = "Üzgünüm, şu anda yanıt veremiyorum. Lütfen sorunuzu farklı şekilde ifade ederek tekrar deneyin.";
                        }
                    }
                    catch (Exception logEx)
                    {
                        _logger.LogError(logEx, "Response loglama hatası. Full Response: {Response}", responseText);
                        content = "Üzgünüm, şu anda yanıt veremiyorum. Lütfen sorunuzu farklı şekilde ifade ederek tekrar deneyin.";
                    }
                }

                var responseDto = new ChatResponseDto
                {
                    Reply = content.Trim(),
                    FromLiveSupport = false,
                    DebugMenuContext = _settings.IncludeDebugMenuContext ? menuJson : null
                };

                _logger.LogInformation("ChatBotService: Yanıt oluşturuldu. Reply Length: {Length}", responseDto.Reply.Length);
                return responseDto;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "DeepSeek API'ye bağlanılamadı. Network hatası veya API erişilemiyor. Request: {RequestMessage}", request.Message);
                return new ChatResponseDto
                {
                    Reply = "Üzgünüm, sohbet servisine şu anda erişilemiyor. Lütfen internet bağlantınızı kontrol edip daha sonra tekrar deneyin.",
                    FromLiveSupport = false
                };
            }
            catch (TaskCanceledException cancelEx)
            {
                _logger.LogError(cancelEx, "DeepSeek API çağrısı zaman aşımına uğradı. Request: {RequestMessage}", request.Message);
                return new ChatResponseDto
                {
                    Reply = "Üzgünüm, yanıt almak çok uzun sürdü. Lütfen tekrar deneyin.",
                    FromLiveSupport = false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeepSeek çağrısı sırasında beklenmeyen bir istisna oluştu. Request: {RequestMessage}, ExceptionType: {ExceptionType}", 
                    request.Message, ex.GetType().Name);

                return new ChatResponseDto
                {
                    Reply = "Üzgünüm, bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
                    FromLiveSupport = false
                };
            }
        }

        private async Task<List<ChatMenuItemDto>> GetActiveMenuItemsAsync(CancellationToken cancellationToken)
        {
            var query = from u in _db.Urun
                        join m in _db.Menu on u.MenuId equals m.Id
                        where u.BirimFiyati2 > 0
                        select new { Urun = u, Menu = m };

            var rawList = await query.ToListAsync(cancellationToken);

            var list = new List<ChatMenuItemDto>();
            foreach (var item in rawList)
            {
                var desc = item.Urun.Aciklama ?? string.Empty;
                var tags = InferTagsFromDescription(desc);

                list.Add(new ChatMenuItemDto
                {
                    Id = item.Urun.Id,
                    Name = item.Urun.UrunAdi ?? string.Empty,
                    Category = item.Menu.MenuAdi ?? string.Empty,
                    Description = desc,
                    Price = item.Urun.BirimFiyati2,
                    Ingredients = desc,
                    Calories = null,
                    Tags = tags
                });
            }

            return list;
        }

        private static List<string> InferTagsFromDescription(string description)
        {
            var tags = new List<string>();
            var text = (description ?? string.Empty).ToLowerInvariant();

            if (text.Contains("vegan")) tags.Add("vegan");
            if (text.Contains("vejetaryen") || text.Contains("vegetarian")) tags.Add("vegetarian");
            if (text.Contains("glutensiz") || text.Contains("gluten-free")) tags.Add("gluten_free");
            if (text.Contains("fıstık") || text.Contains("yer fıstığı") || text.Contains("peanut")) tags.Add("contains_peanut");
            if (text.Contains("badem") || text.Contains("fındık") || text.Contains("ceviz") || text.Contains("nut")) tags.Add("contains_nuts");
            if (text.Contains("protein") || text.Contains("yüksek protein")) tags.Add("high_protein");

            return tags.Distinct().ToList();
        }

        private string BuildSystemPrompt(string menuJson)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"You are a helpful, knowledgeable waiter and nutritionist for {_settings.RestaurantName}.");
            sb.AppendLine("Your job is to help customers choose dishes and drinks from the restaurant menu.");
            sb.AppendLine();
            sb.AppendLine("RULES:");
            sb.AppendLine("- ONLY recommend items that exist in the provided MENU_DATA.");
            sb.AppendLine("- If the user mentions any allergy (especially peanuts or nuts), NEVER recommend items whose ingredients or description contain those allergens.");
            sb.AppendLine("- If the user says 'I have a peanut allergy', strictly avoid and warn about any items tagged with 'contains_peanut' or similar, or whose ingredients suggest peanuts.");
            sb.AppendLine("- If the user says 'I am a bodybuilder' or 'I want to build muscle', prioritize high-protein items (tags: 'high_protein' or protein-rich description).");
            sb.AppendLine("- If the user says 'I am vegan', only recommend items with no animal products (prefer items tagged 'vegan' or clearly plant-based).");
            sb.AppendLine("- Always be polite and concise. Answer in the same language as the user when possible (if the user writes in Turkish, answer in Turkish).");
            sb.AppendLine("- When appropriate, gently upsell drinks and desserts from the menu, but never be pushy.");
            sb.AppendLine("- If the user asks for something that is NOT on the menu, politely explain that you can only recommend items from the current menu and suggest the closest available alternatives.");
            sb.AppendLine();
            sb.AppendLine("OUTPUT FORMAT:");
            sb.AppendLine("- Speak as a friendly waiter.");
            sb.AppendLine("- When recommending, mention the item name, category, price, and a short explanation.");
            sb.AppendLine();
            sb.AppendLine("MENU_DATA (JSON):");
            sb.AppendLine(menuJson);

            return sb.ToString();
        }
    }
}
