# 🏗️ RESTORAN OTOMASYONU - MİMARİ DÖNÜŞÜM PLANI

## 📋 MEVCUT YAPI

```
Restoran Otomasyonu/
├── RestoranOtomasyonu.Entities/          (.NET Framework 4.7.2)
│   ├── Models/                          (Entity sınıfları)
│   ├── DAL/                             (Data Access Layer)
│   ├── Repository/                      (Generic Repository)
│   ├── Mapping/                         (EF6 Fluent API)
│   └── Validations/                      (FluentValidation)
│
└── RestoranOtomasyonu.WinForms/         (.NET Framework 4.7.2)
    ├── AnaMenu/                         (RibbonForm)
    ├── Kullanicilar/                    (CRUD Forms)
    ├── Masalar/                         (Table Management)
    ├── MasaHareketleri/                 (Order Management)
    └── Core/                            (Roller, YetkiKontrolu)
```

## 🎯 HEDEF YAPI (5 AŞAMALI DÖNÜŞÜM)

```
Restoran Otomasyonu/
├── RestoranOtomasyonu.Entities/          (.NET Framework 4.7.2)
│   ├── Models/                          (Entity sınıfları - MEVCUT)
│   ├── DAL/                             (Data Access Layer - MEVCUT)
│   ├── Repository/                      (Generic Repository - MEVCUT)
│   ├── Mapping/                         (EF6 Fluent API - MEVCUT)
│   ├── Validations/                     (FluentValidation - MEVCUT)
│   └── Enums/                          (YENİ: OdemeDurumu, SiparisDurumu)
│
├── RestoranOtomasyonu.WinForms/         (.NET Framework 4.7.2)
│   ├── AnaMenu/                         (MEVCUT - QR Generator eklenecek)
│   ├── Kullanicilar/                    (MEVCUT)
│   ├── Masalar/                         (MEVCUT - QR Generator eklenecek)
│   ├── MasaHareketleri/                 (MEVCUT - SignalR Client eklenecek)
│   ├── Core/                            (MEVCUT)
│   └── Services/                        (YENİ: SignalRClientService)
│
└── RestoranOtomasyonu.WebAPI/           (YENİ: ASP.NET Core 8.0)
    ├── Controllers/
    │   ├── AuthController.cs            (Register, Login)
    │   ├── MenuController.cs            (Ürün listesi)
    │   └── .cs          (Sipariş CRUD)
    ├── Hubs/
    │   └── SiparisHub.cs                (SignalR - Real-time sipariş bildirimleri)
    ├── Services/
    │   └── OrderService.cs              (Business Logic)
    ├── wwwroot/
    │   ├── qr/                          (QR kod sayfaları)
    │   │   └── masa/{id}.html           (QR ile açılan sayfa)
    │   └── js/
    │       ├── app.js                   (Frontend JS)
    │       └── signalr.js               (SignalR Client)
    └── Program.cs                       (API + SignalR + Static Files)
```

## 🔄 VERİTABANI DEĞİŞİKLİKLERİ

### YENİ TABLO: `Siparisler`

```sql
CREATE TABLE Siparisler (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MasaId INT NOT NULL,
    KullaniciId INT NOT NULL,              -- Hangi müşteri sipariş etti?
    SatisKodu VARCHAR(15),
    Tutar DECIMAL(18,2) NOT NULL,
    IndirimOrani DECIMAL(5,2) DEFAULT 0,
    NetTutar DECIMAL(18,2) NOT NULL,
    OdemeDurumu INT NOT NULL,              -- Enum: 0=Odenmedi, 1=KendiOdedi, 2=TumuOdendi
    Aciklama VARCHAR(300),
    Tarih DATETIME NOT NULL DEFAULT GETDATE(),
    
    FOREIGN KEY (MasaId) REFERENCES Masalar(Id),
    FOREIGN KEY (KullaniciId) REFERENCES Kullanicilar(Id)
);
```

### ENUM DEĞERLERİ

```csharp
// OdemeDurumu Enum
public enum OdemeDurumu
{
    Odenmedi = 0,      // Henüz ödenmedi
    KendiOdedi = 1,    // Müşteri kendi payını ödedi
    TumuOdendi = 2     // Masadaki tüm siparişler ödendi
}

// SiparisDurumu Enum (gelecekte kullanılabilir)
public enum SiparisDurumu
{
    Beklemede = 0,
    Hazirlaniyor = 1,
    Hazir = 2,
    TeslimEdildi = 3,
    Iptal = 4
}
```

## 📦 PROJE BAĞIMLILIKLARI

### RestoranOtomasyonu.WebAPI
- **Target Framework:** .NET 8.0
- **NuGet Paketleri:**
  - `Microsoft.AspNetCore.SignalR` (SignalR Hub)
  - `Microsoft.AspNetCore.SignalR.Client` (Client için - WinForms'ta kullanılacak)
  - `Microsoft.EntityFrameworkCore.SqlServer` (Entity Framework Core - Entity'leri kullanmak için)
  - `Microsoft.AspNetCore.Cors` (CORS desteği)
  - `Swashbuckle.AspNetCore` (Swagger UI)

### RestoranOtomasyonu.WinForms
- **Yeni NuGet Paketleri:**
  - `Microsoft.AspNetCore.SignalR.Client` (SignalR Client)
  - `QRCoder` veya `ZXing.Net` (QR Kod oluşturma)

## 🔐 GÜVENLİK VE YETKİLENDİRME

### Web API Authentication
- **Basit Token/Session:** JWT veya Session-based (başlangıç için basit)
- **Rol Kontrolü:** `Roller.Musteri` kontrolü tüm endpoint'lerde

### CORS Ayarları
```csharp
// Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

## 🌐 API ENDPOINT'LERİ

### 1. Authentication
- `POST /api/auth/register` → Müşteri kaydı
- `POST /api/auth/login` → Giriş (Token döner)

### 2. Menu
- `GET /api/menu` → Tüm ürünleri getir

### 3. Orders
- `POST /api/order/create` → Yeni sipariş oluştur
- `GET /api/order/table/{masaId}` → Masadaki TÜM siparişler
- `GET /api/order/my/{kullaniciId}` → Sadece kullanıcının siparişleri
- `POST /api/order/pay/{siparisId}` → Sipariş ödemesi (kendi payını öde)

## 📱 MOBİL ARAYÜZ AKIŞI

1. **QR Kod Okutma:** `http://{ServerIP}/masa/{masaId}`
2. **Login/Register:** Split-screen tasarım
3. **Sipariş Ekranı:** 3 Sekme
   - **Menü:** Ürün listesi + Sepet
   - **Benim Hesabım:** Kullanıcının siparişleri + "Kendi Payımı Öde"
   - **Masa Özeti:** Tüm masanın siparişleri (ReadOnly)

## 🖥️ MASAÜSTÜ GÜNCELLEMELERİ

### QR Oluşturucu
- **Lokasyon:** `frmMasalar` veya `frmAnaMenu`
- **Format:** `http://{LAN_IP}/masa/{MasaID}`
- **Kütüphane:** QRCoder veya ZXing.Net

### SignalR Client
- **Service:** `SignalRClientService.cs`
- **Bağlantı:** `Program.cs` veya `frmAnaMenu_Load`
- **Event:** `ReceiveOrder` → Sesli uyarı + Grid yenileme

### Alman Usulü Görünüm
- **Form:** `frmMasaHareketleri` veya yeni `frmMasaDetay`
- **Grid Gruplama:** `KullaniciAdi` bazlı
- **Sütunlar:** Kullanıcı, Ürün, Miktar, Fiyat, Ödeme Durumu

## 🚀 UYGULAMA SIRASI

1. ✅ **Solution Yapısı Planlama** (Bu doküman)
2. ⏳ **Siparisler Entity + Enum oluştur**
3. ⏳ **ASP.NET Core Web API projesi ekle**
4. ⏳ **Web API Controllers + SignalR Hub**
5. ⏳ **Web Frontend (QR Login + Sipariş)**
6. ⏳ **Masaüstü QR Generator**
7. ⏳ **Masaüstü SignalR Client**
8. ⏳ **Alman Usulü Görünüm**

---

**Not:** Entity Framework 6 (.NET Framework) ile Entity Framework Core (.NET 8) aynı veritabanını paylaşacak. Bu nedenle migration'ları dikkatli yönetmeliyiz.