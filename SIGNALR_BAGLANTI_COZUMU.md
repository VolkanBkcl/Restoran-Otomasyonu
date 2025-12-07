# SignalR Bağlantı Hatası Çözüm Kılavuzu

## 🔍 Sorun Tespiti

SignalR bağlantı hatası genellikle şu nedenlerden kaynaklanır:
1. **Port Uyumsuzluğu**: WinForms uygulaması yanlış porta bağlanıyor
2. **Web API Çalışmıyor**: Web API projesi başlatılmamış
3. **CORS Hatası**: Cross-Origin Resource Sharing ayarları yanlış
4. **Firewall/Network**: Port engellenmiş olabilir

## ✅ Yapılan Düzeltmeler

### 1. Port Düzeltmesi
- **Önceki**: `http://localhost:5000`
- **Yeni**: `http://localhost:5146` (Web API'nin varsayılan portu)

### 2. CORS Yapılandırması
- SignalR için `AllowCredentials()` eklendi
- `AllowAnyOrigin()` yerine `SetIsOriginAllowed(_ => true)` kullanıldı

### 3. Hata Yönetimi İyileştirmeleri
- Daha detaylı debug logları eklendi
- Otomatik yeniden bağlanma mekanizması iyileştirildi
- Retry interval'ları ayarlandı: 0s, 2s, 10s, 30s

## 🚀 Kullanım Adımları

### Adım 1: Web API'yi Başlat
1. Visual Studio'da **RestoranOtomasyonu.WebAPI** projesini seçin
2. **F5** veya **Ctrl+F5** ile çalıştırın
3. Tarayıcıda `http://localhost:5146/swagger` açılmalı
4. Veya `http://localhost:5146/health` endpoint'ini test edin

### Adım 2: WinForms Uygulamasını Başlat
1. **RestoranOtomasyonu.WinForms** projesini çalıştırın
2. Giriş yapın
3. **Output** penceresinde (View → Output) SignalR bağlantı loglarını kontrol edin

### Adım 3: Bağlantıyı Test Et
- **Başarılı**: `SignalR bağlantısı başarılı! ConnectionId: ...`
- **Başarısız**: `SignalR bağlantı hatası: ...` (Web API çalışmıyor olabilir)

## 🔧 Sorun Giderme

### Problem 1: "Connection refused" veya "No connection could be made"
**Çözüm**: Web API çalışmıyor. Web API projesini başlatın.

### Problem 2: "CORS policy" hatası
**Çözüm**: Web API'de CORS ayarları doğru yapılandırılmış. Eğer hala hata alıyorsanız:
```csharp
// Program.cs'de CORS ayarlarını kontrol edin
policy.SetIsOriginAllowed(_ => true)
      .AllowCredentials();
```

### Problem 3: Port değişti
**Çözüm**: Eğer Web API farklı bir portta çalışıyorsa:
1. `launchSettings.json` dosyasındaki portu kontrol edin
2. `Program.cs` dosyasındaki `apiBaseUrl` değerini güncelleyin

### Problem 4: Firewall Engellemesi
**Çözüm**: Windows Firewall'da port 5146'yı açın:
```powershell
New-NetFirewallRule -DisplayName "Web API SignalR" -Direction Inbound -LocalPort 5146 -Protocol TCP -Action Allow
```

## 📝 Debug Logları

SignalR bağlantı durumunu görmek için:
1. Visual Studio'da **View → Output** menüsünü açın
2. **Show output from:** dropdown'ından **Debug** seçin
3. Şu logları göreceksiniz:
   - `SignalR bağlantısı deneniyor: http://localhost:5146/siparisHub`
   - `SignalR bağlantısı başarılı! ConnectionId: ...`
   - Veya hata mesajları

## 🎯 Test Senaryosu

1. **Web API'yi başlatın** → `http://localhost:5146/swagger` açılmalı
2. **WinForms uygulamasını başlatın** → Giriş yapın
3. **Output penceresinde** SignalR bağlantı loglarını kontrol edin
4. **Web tarayıcısında** QR kod sayfasını açın (`http://localhost:5146/masa/1`)
5. **Sipariş verin** → WinForms uygulamasında bildirim gelmeli

## ⚠️ Önemli Notlar

- Web API **her zaman** WinForms uygulamasından **önce** başlatılmalı
- Eğer Web API çalışmıyorsa, SignalR bağlantısı otomatik olarak yeniden deneyecek
- Production ortamında URL'yi `appsettings.json`'dan alın

## 🔄 Alternatif Port Kullanımı

Eğer 5146 portu kullanılamıyorsa:

1. **Web API `launchSettings.json`** dosyasını düzenleyin:
```json
"applicationUrl": "http://localhost:5000"
```

2. **WinForms `Program.cs`** dosyasını güncelleyin:
```csharp
string apiBaseUrl = "http://localhost:5000";
```

