# Chatbot Debug Rehberi

## Sorun: Chatbot sürekli aynı cevabı veriyor

### Adım 1: Console'da Test

Browser Console'da (F12 → Console) şu kodu çalıştırın:

```javascript
// Test 1: API_BASE_URL kontrolü
console.log('API_BASE_URL:', window.location.origin);

// Test 2: sendChatMessage fonksiyonu var mı?
console.log('sendChatMessage:', typeof sendChatMessage);

// Test 3: Manuel API çağrısı
fetch('/api/chat/send', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify({
        message: "test mesajı",
        isLiveSupportRequest: false,
        sessionId: "test-session-" + Date.now(),
        userName: "Test User"
    })
})
.then(response => {
    console.log('Status:', response.status);
    console.log('Status Text:', response.statusText);
    return response.json();
})
.then(data => {
    console.log('✅ Response:', data);
    console.log('✅ Reply:', data.reply);
    if (!data.reply || data.reply.includes('Üzgünüm')) {
        console.warn('⚠️ Yanıt boş veya hata mesajı içeriyor!');
    }
})
.catch(error => {
    console.error('❌ Error:', error);
});
```

### Adım 2: Network Tab Kontrolü

1. **Filtreyi kaldırın** - Filtre kutusunu boş bırakın
2. **Network kayıtlarını temizleyin** - "Clear" butonuna tıklayın
3. **Network tab'ı açıkken mesaj gönderin**
4. **Tüm istekleri kontrol edin** - Özellikle:
   - `send` içeren istekler
   - `chat` içeren istekler
   - `api` içeren istekler

### Adım 3: WebAPI Loglarını Kontrol

Visual Studio'da WebAPI çalışırken Output penceresinde şu logları arayın:

- `ChatController.Send çağrıldı`
- `Gemini API'ye istek gönderiliyor`
- `Gemini API yanıtı alındı`
- `ChatBotService: Yanıt oluşturuldu`

### Adım 4: Test Endpoint

Tarayıcıda şu URL'yi açın:
```
http://localhost:5146/api/chat/test
```

Bu endpoint Gemini API'yi test eder.

### Olası Sorunlar ve Çözümler

1. **API Key geçersiz**
   - `appsettings.json` dosyasında `OpenAI:ApiKey` kontrol edin
   - Gemini API key'in geçerli olduğundan emin olun

2. **Network hatası**
   - Internet bağlantısını kontrol edin
   - WebAPI'nin çalıştığından emin olun

3. **Parse hatası**
   - Gemini'den gelen yanıt yapısı beklenenden farklı olabilir
   - WebAPI loglarında "Gemini response parse" mesajlarını kontrol edin

4. **Boş yanıt**
   - Gemini'den boş yanıt geliyor olabilir
   - WebAPI loglarında "Gemini'den boş yanıt alındı" mesajını kontrol edin
