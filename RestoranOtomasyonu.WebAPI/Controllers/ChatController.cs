using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestoranOtomasyonu.WebAPI.Data;
using RestoranOtomasyonu.WebAPI.Models;
using RestoranOtomasyonu.WebAPI.Services;

namespace RestoranOtomasyonu.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly RestaurantAiDbContext _db;
        private readonly IChatBotService _chatBotService;
        private readonly ILogger<ChatController> _logger;
        private readonly IConfiguration _configuration;

        public ChatController(
            RestaurantAiDbContext db,
            IChatBotService chatBotService,
            ILogger<ChatController> logger,
            IConfiguration configuration)
        {
            _db = db;
            _chatBotService = chatBotService;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("send")]
        public async Task<ActionResult<ChatResponseDto>> Send([FromBody] ChatRequestDto request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ChatController.Send çağrıldı. Request null mu: {IsNull}, Message: '{Message}'", 
                request == null, request?.Message ?? "NULL");

            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                _logger.LogWarning("ChatController.Send: Geçersiz istek - request null veya mesaj boş.");
                return BadRequest(new { message = "Mesaj boş olamaz." });
            }

            _logger.LogInformation("ChatController.Send: İşleniyor. Message: '{Message}', IsLiveSupport: {IsLiveSupport}, SessionId: {SessionId}", 
                request.Message, request.IsLiveSupportRequest, request.SessionId);

            var now = DateTime.UtcNow;

            try
            {
                if (request.IsLiveSupportRequest)
                {
                    var chatMessage = new ChatMessage
                    {
                        // Gerekli alanlar: Sender, Message, Timestamp, IsLiveSupportRequest
                        Sender = request.UserName ?? "customer",
                        Message = request.Message,
                        Timestamp = now,
                        IsLiveSupportRequest = true,
                        SessionId = request.SessionId,
                        UserName = request.UserName
                    };

                    // Canlı destek mesajını kaydetmeyi dene (opsiyonel - tablo yoksa devam et)
                    try
                    {
                        _db.ChatMessages.Add(chatMessage);
                        await _db.SaveChangesAsync(cancellationToken);
                        _logger.LogDebug("Canlı destek mesajı kaydedildi. MessageId: {Id}", chatMessage.Id);
                    }
                    catch (DbUpdateException dbEx)
                    {
                        var inner = dbEx.InnerException?.Message;
                        // Tablo yoksa veya başka bir DB hatası varsa, sadece logla ve devam et
                        _logger.LogWarning(dbEx, "Canlı destek mesajı kaydedilemedi (opsiyonel). Inner: {Inner}. Kullanıcıya yanıt verilecek.", inner);
                        // Hata olsa bile kullanıcıya yanıt verilecek
                    }

                    var response = new ChatResponseDto
                    {
                        Reply = "Mesajınızı aldım, sizi hemen canlı destek görevlisine bağlıyorum. Lütfen birkaç saniye bekleyin.",
                        FromLiveSupport = true
                    };

                    return Ok(response);
                }
                else
                {
                    var userMessage = new ChatMessage
                    {
                        // Gerekli alanlar: Sender, Message, Timestamp, IsLiveSupportRequest
                        Sender = request.UserName ?? "customer",
                        Message = request.Message,
                        Timestamp = now,
                        IsLiveSupportRequest = false,
                        SessionId = request.SessionId,
                        UserName = request.UserName
                    };

                    // Mesaj kaydetmeyi dene (opsiyonel - tablo yoksa devam et)
                    try
                    {
                        _db.ChatMessages.Add(userMessage);
                        await _db.SaveChangesAsync(cancellationToken);
                        _logger.LogDebug("Kullanıcı chat mesajı kaydedildi. MessageId: {Id}", userMessage.Id);
                    }
                    catch (DbUpdateException dbEx)
                    {
                        var inner = dbEx.InnerException?.Message;
                        // Tablo yoksa veya başka bir DB hatası varsa, sadece logla ve devam et
                        _logger.LogWarning(dbEx, "Kullanıcı chat mesajı kaydedilemedi (opsiyonel). Inner: {Inner}. Chatbot yanıt vermeye devam edecek.", inner);
                        // Hata olsa bile chatbot yanıt vermeye devam edecek
                    }

                    _logger.LogInformation("ChatController.Send: AI servisine istek gönderiliyor.");
                    var aiResponse = await _chatBotService.GetAiResponseAsync(request, cancellationToken);
                    _logger.LogInformation("ChatController.Send: AI yanıtı alındı. Reply Length: {Length}", aiResponse?.Reply?.Length ?? 0);

                    var aiMessage = new ChatMessage
                    {
                        Sender = "ai",
                        Message = aiResponse.Reply,
                        Timestamp = DateTime.UtcNow,
                        IsLiveSupportRequest = false,
                        SessionId = request.SessionId,
                        UserName = request.UserName
                    };

                    // AI yanıtını kaydetmeyi dene (opsiyonel - tablo yoksa devam et)
                    try
                    {
                        _db.ChatMessages.Add(aiMessage);
                        await _db.SaveChangesAsync(cancellationToken);
                        _logger.LogDebug("AI chat mesajı kaydedildi. MessageId: {Id}", aiMessage.Id);
                    }
                    catch (DbUpdateException dbEx)
                    {
                        var inner = dbEx.InnerException?.Message;
                        // Tablo yoksa veya başka bir DB hatası varsa, sadece logla ve devam et
                        _logger.LogWarning(dbEx, "AI cevabı kaydedilemedi (opsiyonel). Inner: {Inner}. Yanıt kullanıcıya iletilecek.", inner);
                        // Hata olsa bile yanıt kullanıcıya iletilecek
                    }

                    return Ok(aiResponse);
                }
            }
            catch (DbUpdateException dbEx)
            {
                var inner = dbEx.InnerException?.Message;
                _logger.LogError(dbEx, "Chat mesajı kaydedilirken DB hatası (outer catch). Inner: {Inner}", inner);
                return StatusCode(500, new
                {
                    message = "Mesaj kaydedilirken veritabanı hatası oluştu.",
                    error = dbEx.Message,
                    innerError = inner
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ChatController.Send sırasında hata oluştu.");
                return StatusCode(500, new { message = "Chat işlemi sırasında hata oluştu.", error = ex.Message });
            }
        }

        /// <summary>
        /// Test endpoint - Gemini API bağlantısını test et
        /// </summary>
        [HttpGet("test")]
        public async Task<ActionResult> TestGeminiConnection(CancellationToken cancellationToken)
        {
            try
            {
                var testRequest = new ChatRequestDto
                {
                    Message = "Merhaba, test mesajı",
                    IsLiveSupportRequest = false,
                    SessionId = "test-session",
                    UserName = "Test User"
                };

                _logger.LogInformation("Test endpoint çağrıldı. Gemini API test ediliyor...");
                var response = await _chatBotService.GetAiResponseAsync(testRequest, cancellationToken);
                
                return Ok(new
                {
                    success = true,
                    reply = response.Reply,
                    replyLength = response.Reply?.Length ?? 0,
                    fromLiveSupport = response.FromLiveSupport,
                    message = "Gemini API bağlantısı başarılı!"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Test endpoint hatası.");
                return StatusCode(500, new
                {
                    success = false,
                    error = ex.Message,
                    message = "Gemini API test başarısız!"
                });
            }
        }

        /// <summary>
        /// Mevcut Gemini modellerini listele
        /// </summary>
        [HttpGet("list-models")]
        public async Task<ActionResult> ListGeminiModels(CancellationToken cancellationToken)
        {
            try
            {
                using var httpClient = new System.Net.Http.HttpClient();
                var apiKey = _configuration["OpenAI:ApiKey"];
                var baseUrl = _configuration["OpenAI:BaseUrl"] ?? "https://generativelanguage.googleapis.com/v1beta";
                
                var listUrl = $"{baseUrl}/models?key={apiKey}";
                _logger.LogInformation("Gemini modelleri listeleniyor. URL: {Url}", listUrl.Replace(apiKey, "***"));
                
                var response = await httpClient.GetAsync(listUrl, cancellationToken);
                var responseText = await response.Content.ReadAsStringAsync(cancellationToken);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Model listesi alınamadı. Status: {Status}, Response: {Response}", 
                        response.StatusCode, responseText);
                    return StatusCode((int)response.StatusCode, new { error = responseText });
                }
                
                return Ok(new { models = responseText, raw = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Model listesi hatası.");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
