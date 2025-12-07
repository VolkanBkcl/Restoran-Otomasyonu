# Veritabanı Bağlantı Düzeltmesi

## 🔍 Tespit Edilen Sorunlar

1. **Parola Hash Uyumsuzluğu:**
   - WinForms uygulaması parolayı **hash'lemeden** kaydediyor
   - Web API parolayı **MD5 ile hash'leyerek** kontrol ediyordu
   - Bu yüzden mevcut kullanıcılar giriş yapamıyordu

2. **Connection String:**
   - AuthController'da connection string hardcoded olarak yazılmıştı
   - appsettings.json'dan alınmıyordu

## ✅ Yapılan Düzeltmeler

### 1. Parola Kontrolü Düzeltildi
- **Önceki:** Sadece hash'lenmiş parola ile kontrol
- **Yeni:** Hem hash'lenmemiş hem hash'lenmiş parola ile kontrol (backward compatibility)
- WinForms'tan kaydedilen kullanıcılar artık giriş yapabilir

### 2. Connection String Düzeltildi
- **Önceki:** Hardcoded connection string
- **Yeni:** `appsettings.json`'dan alınıyor
- `IConfiguration` dependency injection ile kullanılıyor

### 3. Logging Eklendi
- Giriş başarılı/başarısız durumları loglanıyor
- Debug için faydalı

### 4. Test Endpoint'i Eklendi
- `/test-db` endpoint'i ile veritabanı bağlantısı test edilebilir

## 🧪 Test Etme

### 1. Veritabanı Bağlantısını Test Et
Tarayıcıda veya Postman'de:
```
GET http://localhost:5146/test-db
```

**Başarılı yanıt:**
```json
{
  "status": "OK",
  "message": "Veritabanı bağlantısı başarılı",
  "kullaniciSayisi": 5,
  "connectionString": "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true"
}
```

### 2. Giriş Yapmayı Test Et
Web sayfasında (`http://localhost:5146/masa/1`):
- Kullanıcı adı: `Volkan_174`
- Parola: (WinForms'ta kaydedilen parola)

**Beklenen sonuç:** Giriş başarılı olmalı

### 3. Swagger'da Test Et
```
POST http://localhost:5146/api/auth/login
Content-Type: application/json

{
  "kullaniciAdi": "Volkan_174",
  "parola": "şifreniz"
}
```

## 📝 Önemli Notlar

### Güvenlik Uyarısı
⚠️ **Şu anda parolalar hash'lenmeden saklanıyor!**

**Production ortamında mutlaka:**
1. Parolaları hash'leyin (SHA256 veya bcrypt)
2. WinForms uygulamasında da aynı hash yöntemini kullanın
3. Mevcut kullanıcıların parolalarını hash'leyerek güncelleyin

### Geçici Çözüm
Şu anki kod hem hash'lenmemiş hem hash'lenmiş parolayı kabul ediyor. Bu sayede:
- Eski kullanıcılar (hash'lenmemiş parola) giriş yapabilir
- Yeni kullanıcılar (hash'lenmiş parola) giriş yapabilir

### Önerilen Migration Stratejisi
1. Tüm mevcut parolaları hash'leyin
2. WinForms ve Web API'de aynı hash yöntemini kullanın
3. Yeni kayıtlarda otomatik hash'leme yapın

## 🔧 Connection String Ayarları

`appsettings.json` dosyasında:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restoran;Integrated Security=true"
  }
}
```

**Farklı bir veritabanı kullanıyorsanız:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Restoran;User Id=sa;Password=şifreniz;TrustServerCertificate=true;"
  }
}
```

## 🐛 Sorun Giderme

### Problem: "Kullanıcı adı veya parola hatalı"
**Çözüm:**
1. Veritabanında kullanıcıyı kontrol edin:
   ```sql
   SELECT KullaniciAdi, Parola FROM Kullanicilar WHERE KullaniciAdi = 'Volkan_174'
   ```
2. Parolanın hash'lenmemiş olduğundan emin olun
3. `/test-db` endpoint'i ile veritabanı bağlantısını test edin

### Problem: "Veritabanı bağlantı hatası"
**Çözüm:**
1. SQL Server'ın çalıştığından emin olun
2. Connection string'i kontrol edin
3. Veritabanının mevcut olduğundan emin olun:
   ```sql
   SELECT name FROM sys.databases WHERE name = 'Restoran'
   ```

## 📊 Test Sonuçları

Başarılı test için:
- ✅ `/test-db` endpoint'i kullanıcı sayısını döndürüyor
- ✅ `/api/auth/login` endpoint'i başarılı giriş yapıyor
- ✅ Web sayfasında giriş yapılabiliyor

