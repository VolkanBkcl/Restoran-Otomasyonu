# Müşteri HTML Sayfaları - Kullanım Kılavuzu

## 📁 Dosya Yapısı

```
RestoranOtomasyonu.WebAPI/
└── wwwroot/
    ├── masa/
    │   └── index.html          # Ana müşteri sayfası (QR kod ile açılır)
    ├── css/
    │   └── style.css           # Tüm stiller (modern, responsive)
    └── js/
        └── app.js              # Tüm JavaScript fonksiyonları
```

## 🎯 Özellikler

### 1. **Giriş/Kayıt Ekranı (Split-Screen Design)**
- ✅ **Sol Panel**: Statik logo/hoş geldiniz mesajı
- ✅ **Sağ Panel**: 
  - Giriş Yap formu
  - Kayıt Ol formu
  - Şifremi Unuttum formu (3 adımlı)

**Giriş Formu:**
- Kullanıcı Adı
- Parola
- Beni Hatırla checkbox
- Şifremi Unuttum linki
- Kayıt Ol linki

**Kayıt Formu:**
- Ad Soyad *
- Telefon
- Email
- Kullanıcı Adı *
- Parola *
- Hatırlatma Sorusu (dropdown veya custom)
- Cevap
- Geri Dön linki

**Şifremi Unuttum:**
- Adım 1: Kullanıcı adı gir
- Adım 2: Güvenlik sorusunu cevapla
- Adım 3: Yeni şifre belirle

### 2. **Sipariş Ekranı (3 Sekme)**

#### 📋 Sekme 1: Menü
- **Menü Filtreleme:**
  - Arama kutusu (ürün adına göre)
  - Kategori filtresi (dropdown)
- **Ürün Kartları:**
  - Ürün resmi (placeholder ile)
  - Ürün adı
  - Fiyat
  - "Sepete Ekle" butonu
- **Sepet Bölümü:**
  - Sepet öğeleri listesi
  - Miktar artırma/azaltma
  - Ürün silme
  - Ara toplam
  - KDV hesaplama (%20)
  - Genel toplam
  - "Sipariş Ver" butonu (sepet boşsa disabled)

#### 👤 Sekme 2: Benim Hesabım
- **Kullanıcı Profili:**
  - Avatar (isim baş harfleri)
  - Ad Soyad
  - Email
- **Siparişlerim:**
  - Filtre butonları (Tümü, Bekleyen, Ödenen)
  - Sipariş kartları:
    - Sipariş kodu
    - Masa bilgisi
    - Ödeme durumu (badge)
    - Tutar bilgileri
    - Tarih
    - "Kendi Payımı Öde" butonu (sadece ödenmemiş siparişler için)

#### 📊 Sekme 3: Masa Özeti
- **İstatistikler:**
  - Kişi sayısı
  - Sipariş sayısı
  - Toplam tutar
- **Masadaki Siparişler:**
  - Kullanıcı adına göre gruplandırılmış
  - Her sipariş için:
    - Kullanıcı adı
    - Sipariş kodu
    - Ödeme durumu
    - Tutar bilgileri
    - Tarih
- **Genel Toplam:**
  - Ara toplam
  - KDV
  - Final toplam

## 🎨 Tasarım Özellikleri

### Renkler
- **Ana Renk**: `#667eea` (Mor-mavi gradient)
- **Başarı**: `#28a745` (Yeşil)
- **Hata**: `#dc3545` (Kırmızı)
- **Uyarı**: `#ffc107` (Sarı)

### Responsive Tasarım
- ✅ Mobil uyumlu (768px altında tek sütun)
- ✅ Tablet uyumlu
- ✅ Desktop uyumlu

### Animasyonlar
- ✅ Hover efektleri
- ✅ Loading spinner
- ✅ Toast bildirimleri (slide-in)
- ✅ Smooth transitions

## 🔌 API Entegrasyonu

### Kullanılan Endpoint'ler:
- `POST /api/auth/login` - Giriş
- `POST /api/auth/register` - Kayıt
- `POST /api/auth/forgot-password/check` - Güvenlik sorusu getir
- `POST /api/auth/forgot-password/verify` - Cevap doğrula
- `POST /api/auth/forgot-password/reset` - Şifre sıfırla
- `GET /api/menu` - Menü listesi
- `POST /api/order/create` - Sipariş oluştur
- `GET /api/order/my/{kullaniciId}` - Kullanıcı siparişleri
- `GET /api/order/table/{masaId}` - Masa siparişleri
- `POST /api/order/pay/{siparisId}` - Ödeme yap

## 📱 Kullanım Senaryosu

1. **QR Kod Okutma:**
   - Müşteri masadaki QR kodu okutur
   - URL: `http://{ServerIP}/masa/{MasaID}`
   - Sayfa açılır

2. **Giriş/Kayıt:**
   - Yeni müşteri: "Kayıt Ol" sekmesine tıklar, formu doldurur
   - Mevcut müşteri: "Giriş Yap" sekmesinde giriş yapar
   - Şifre unutma: "Şifremi Unuttum" linkine tıklar, 3 adımlı süreci tamamlar

3. **Sipariş Verme:**
   - Menü sekmesinde ürünleri görüntüler
   - İstediği ürünleri sepete ekler
   - Sepeti kontrol eder
   - "Sipariş Ver" butonuna tıklar
   - Sipariş oluşturulur ve masaüstü uygulamasına bildirim gider

4. **Ödeme:**
   - "Benim Hesabım" sekmesinde kendi siparişlerini görür
   - "Kendi Payımı Öde" butonuna tıklar
   - Ödeme yapılır (Alman usulü - herkes kendi payını öder)

5. **Masa Özeti:**
   - "Masa Özeti" sekmesinde masadaki tüm siparişleri görür
   - Kim ne kadar ödedi bilgisini görür
   - Genel toplamı görür

## 🚀 Test Etme

1. **Web API'yi başlatın:**
   ```bash
   cd RestoranOtomasyonu.WebAPI
   dotnet run
   ```

2. **Tarayıcıda açın:**
   ```
   http://localhost:5146/masa/1
   ```

3. **Test senaryoları:**
   - ✅ Yeni kullanıcı kaydı
   - ✅ Giriş yapma
   - ✅ Şifremi unuttum
   - ✅ Menü görüntüleme
   - ✅ Sepete ürün ekleme
   - ✅ Sipariş verme
   - ✅ Siparişleri görüntüleme
   - ✅ Ödeme yapma
   - ✅ Masa özeti görüntüleme

## 📝 Notlar

- **LocalStorage**: Kullanıcı bilgileri tarayıcıda saklanır (Beni Hatırla)
- **SignalR**: Gerçek zamanlı güncellemeler için kullanılabilir (şu an kullanılmıyor, eklenebilir)
- **Resimler**: Ürün resimleri yoksa placeholder SVG gösterilir
- **KDV**: %20 olarak hesaplanıyor (değiştirilebilir)

## 🔧 Özelleştirme

### KDV Oranını Değiştirme:
`wwwroot/js/app.js` dosyasında:
```javascript
const taxRate = 0.20; // %20 KDV
```

### Renkleri Değiştirme:
`wwwroot/css/style.css` dosyasında ana renkleri değiştirin:
```css
/* Ana renk */
#667eea → istediğiniz renk
```

### Logo/Resim Ekleme:
`wwwroot/masa/index.html` dosyasında sol paneldeki logo bölümünü özelleştirin.

## ⚠️ Eksik Özellikler (İsteğe Bağlı)

- [ ] SignalR ile gerçek zamanlı sipariş güncellemeleri
- [ ] Ürün resimlerini yükleme
- [ ] Kategori bazlı filtreleme (API'den kategori listesi gelmeli)
- [ ] Sipariş iptal etme (müşteri için)
- [ ] Sipariş detayları görüntüleme
- [ ] Favori ürünler
- [ ] Önceki siparişleri tekrar sipariş verme

