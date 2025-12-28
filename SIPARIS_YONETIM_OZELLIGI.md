# Gruplandırılmış Masa Siparişleri ve Anlık Durum Takibi - Uygulama Rehberi

## 📋 Özet

Bu dokümantasyon, Restoran Otomasyonu projesine eklenen "Gruplandırılmış Masa Siparişleri ve Anlık Durum Takibi" özelliğinin detaylarını içerir.

## ✅ Tamamlanan Özellikler

### 1. Veritabanı ve Entity Güncellemesi

- ✅ `SiparisDurumu` enum'u güncellendi
  - Yeni değerler: `SiparisAlindi`, `Hazirlaniyor`, `Hazir`, `ServisEdildi`
  - Eski değerlerle uyumluluk korundu
- ✅ `Siparisler` entity'sinde `SiparisDurumu` alanı zaten mevcut

### 2. Backend (DTO ve Mantık)

- ✅ `SiparisGrupDTO` sınıfı oluşturuldu
  - Masa Adı, Toplam Tutar, Durum bilgileri
  - `List<SiparisDetayDTO>` içerir
- ✅ `SiparisDetayDTO` sınıfı oluşturuldu
  - Kullanıcı bilgileri, sipariş detayları
  - `List<SiparisUrunDTO>` içerir
- ✅ `SiparisGrupService` servisi oluşturuldu
  - Gruplandırma mantığı (aynı masa + aynı dakika)
  - Durum güncelleme metodları

### 3. WebAPI Endpoints

- ✅ `GET /api/order/grouped` - Gruplandırılmış siparişleri getir
- ✅ `POST /api/order/updateStatus/{siparisId}` - Tek sipariş durumunu güncelle
- ✅ `POST /api/order/updateGroupStatus` - Grup durumunu güncelle

### 4. WinForms (Garson/Mutfak Ekranı)

- ✅ `frmSiparisYonetim` formu oluşturuldu
  - Gruplandırılmış sipariş listesi
  - Ayrıntılar butonu
  - Durum değiştirme butonu
- ✅ `frmSiparisDetay` formu oluşturuldu
  - Grup içindeki sipariş detaylarını gösterir
- ✅ `frmSiparisDurumSec` formu oluşturuldu
  - Durum seçim ComboBox'ı

### 5. Real-Time (SignalR Entegrasyonu)

- ✅ SignalR Hub'a yeni event'ler eklendi
  - `OrderStatusUpdated` - Tek sipariş durumu güncellemesi
  - `GroupStatusUpdated` - Grup durumu güncellemesi
- ✅ Web tarafında SignalR dinleyicisi eklendi
  - Anlık durum güncellemeleri
  - UI otomatik yenilenir

## 📁 Oluşturulan Dosyalar

### Entities Projesi
- `RestoranOtomasyonu.Entities/DTOs/SiparisGrupDTO.cs`
- `RestoranOtomasyonu.Entities/Services/SiparisGrupService.cs`
- `RestoranOtomasyonu.Entities/Enums/SiparisDurumu.cs` (güncellendi)

### WinForms Projesi
- `RestoranOtomasyonu.WinForms/SiparisYonetim/frmSiparisYonetim.cs`
- `RestoranOtomasyonu.WinForms/SiparisYonetim/frmSiparisDetay.cs`
- `RestoranOtomasyonu.WinForms/SiparisYonetim/frmSiparisDurumSec.cs`

### WebAPI Projesi
- `RestoranOtomasyonu.WebAPI/Controllers/OrderController.cs` (güncellendi)
- `RestoranOtomasyonu.WebAPI/wwwroot/js/app.js` (güncellendi)

## 🔧 Yapılması Gerekenler

### 1. Designer Dosyaları

WinForms formları için Designer dosyaları oluşturulmalı:

#### `frmSiparisYonetim.Designer.cs`
```csharp
// DevExpress GridControl, GridView, Button kontrolleri eklenmeli
```

#### `frmSiparisDetay.Designer.cs`
```csharp
// DevExpress GridControl, Label kontrolleri eklenmeli
```

#### `frmSiparisDurumSec.Designer.cs`
```csharp
// DevExpress ComboBox, Button kontrolleri eklenmeli
```

### 2. Proje Dosyası Güncellemesi

`RestoranOtomasyonu.WinForms.csproj` dosyasına yeni formlar eklenmeli:

```xml
<Compile Include="SiparisYonetim\frmSiparisYonetim.cs">
  <SubType>Form</SubType>
</Compile>
<Compile Include="SiparisYonetim\frmSiparisYonetim.Designer.cs">
  <DependentUpon>frmSiparisYonetim.cs</DependentUpon>
</Compile>
<!-- Diğer formlar için de benzer şekilde -->
```

### 3. NuGet Paketleri

WinForms projesine gerekli paketler zaten yüklü:
- ✅ `Microsoft.AspNetCore.SignalR.Client` (10.0.1)
- ✅ `Newtonsoft.Json` (JSON serialization için)

### 4. Ana Menüye Ekleme

`frmAnaMenu` formuna "Sipariş Yönetimi" menü öğesi eklenmeli:

```csharp
private void btnSiparisYonetim_Click(object sender, EventArgs e)
{
    var frm = new SiparisYonetim.frmSiparisYonetim();
    frm.ShowDialog();
}
```

## 🚀 Kullanım

### WinForms'tan Sipariş Durumu Değiştirme

1. Ana menüden "Sipariş Yönetimi" seçilir
2. Gruplandırılmış sipariş listesi görüntülenir
3. Bir sipariş grubu seçilir
4. "Durum Değiştir" butonuna tıklanır
5. Yeni durum seçilir (Sipariş Alındı → Hazırlanıyor → Hazır → Servis Edildi)
6. Durum WebAPI'ye gönderilir
7. SignalR ile web tarafına bildirim gönderilir

### Web Tarafında Anlık Güncelleme

1. Müşteri web sayfasında siparişlerini görüntüler
2. WinForms'tan durum değiştirildiğinde
3. SignalR ile anlık bildirim gelir
4. UI otomatik olarak güncellenir
5. "Siparişiniz Hazırlanıyor" → "Siparişiniz Hazır" mesajları gösterilir

## 📝 Notlar

- Gruplandırma mantığı: Aynı masa ve aynı dakika içindeki siparişler tek grup olarak gösterilir
- Durum önceliği: Grup içindeki en yüksek öncelikli durum grup durumu olarak gösterilir
- SignalR bağlantısı: Web tarafında otomatik bağlanır, bağlantı kesilirse otomatik yeniden bağlanır

## 🔄 Migration

Enum değerleri güncellendi ancak veritabanı şeması değişmedi. Mevcut `SiparisDurumu` INT kolonu aynı kalır, sadece enum değerleri güncellendi.

Eğer migration oluşturmak isterseniz:

```powershell
# Package Manager Console'da
Add-Migration SiparisDurumuGuncellemesi
Update-Database
```

Ancak bu gerekli değil çünkü veritabanı şeması değişmedi.

## 🐛 Bilinen Sorunlar

- Designer dosyaları henüz oluşturulmadı (manuel olarak Visual Studio'da oluşturulmalı)
- Ana menüye buton eklenmedi (manuel olarak eklenmeli)

## 📞 Destek

Sorularınız için proje dokümantasyonuna bakın veya geliştirici ile iletişime geçin.
