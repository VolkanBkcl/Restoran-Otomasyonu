# Görsel Dosya Adlandırma Rehberi

## 🔍 Sorun
Veritabanında görsel yolları `Image/Çay-001.jpeg` formatında kayıtlı ama klasörde `çay.jpeg` gibi farklı isimlerle dosyalar var.

## ✅ Çözüm 1: Ürünleri Yeniden Kaydetmek (Önerilen)

### Adımlar:
1. WinForms uygulamasında **Ürünler** formunu açın
2. Bir ürünü seçin ve **Düzenle** butonuna tıklayın
3. Görseli seçin (veya aynı görseli tekrar yükleyin)
4. **Kaydet** butonuna tıklayın

### Ne Olur?
- Dosya `ÜrünAdı-ÜrünKodu.jpeg` formatında kaydedilir
- Hem WinForms hem WebAPI klasörüne kopyalanır
- Veritabanı otomatik güncellenir

### Örnek:
- Ürün: Çay (Kod: 001)
- Dosya Adı: `Çay-001.jpeg`
- Veritabanı: `Image/Çay-001.jpeg` ✅

---

## ⚡ Çözüm 2: Dosyaları Manuel Yeniden Adlandırmak

Eğer çok sayıda ürün varsa, dosyaları manuel olarak yeniden adlandırabilirsiniz.

### Dosya Adı Formatı:
```
ÜrünAdı-ÜrünKodu.jpeg
```

### Örnek Eşleştirmeler:

| Klasördeki Mevcut Dosya | Veritabanındaki Ürün | Yeni Dosya Adı |
|------------------------|---------------------|----------------|
| `çay.jpeg` | Çay (001) | `Çay-001.jpeg` |
| `cola.jpeg` | Coca-Cola (002) | `Coca-Cola-002.jpeg` |
| `mercimek çorbası.jpeg` | Mercimek Çorbası (CRB-001) | `Mercimek Çorbası-CRB-001.jpeg` |

### Adımlar:
1. `RestoranOtomasyonu.WebAPI\wwwroot\Image\` klasörüne gidin
2. Dosyaları veritabanındaki ürün adlarına göre yeniden adlandırın
3. Aynı dosyaları `RestoranOtomasyonu.WinForms\bin\Debug\Image\` klasörüne de kopyalayın

---

## 🔧 Çözüm 3: Programatik Çözüm (Gelişmiş)

WinForms uygulamasına bir buton ekleyip tüm görselleri otomatik olarak yeniden adlandırabiliriz. İsterseniz bu özelliği ekleyebilirim.

---

## ⚠️ Önemli Notlar

1. **Yedek Alın**: İşlem öncesi görsellerin yedeğini alın
2. **WebAPI'yi Yeniden Başlatın**: Değişikliklerden sonra WebAPI'yi yeniden başlatın
3. **Tarayıcı Cache**: Tarayıcı cache'ini temizleyin (Ctrl+F5)

---

## 📋 Kontrol Listesi

- [ ] Mevcut görselleri yedekle
- [ ] Ürünleri tek tek açıp görselleri yeniden kaydet VEYA dosyaları yeniden adlandır
- [ ] WebAPI'yi yeniden başlat
- [ ] Sipariş sitesinde görselleri test et
- [ ] Tarayıcı cache'ini temizle

---

## 🎯 Sonuç

Her iki çözüm de çalışır, ancak **Çözüm 1** (ürünleri yeniden kaydetmek) daha güvenli ve otomatiktir. Yeni görseller artık doğru formatla kaydedilecek.

