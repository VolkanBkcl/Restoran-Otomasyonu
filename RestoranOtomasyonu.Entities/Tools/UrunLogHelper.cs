using RestoranOtomasyonu.Entities.Models;
using System;
using System.Globalization;
using System.Text;

namespace RestoranOtomasyonu.Entities.Tools
{
    /// <summary>
    /// Urun (Ürün) işlemlerini UrunHareketleri tablosuna log olarak kaydeden yardımcı sınıf.
    /// </summary>
    public static class UrunLogHelper
    {
        /// <summary>
        /// Urun üzerinde yapılan işlemi (ekleme / güncelleme / silme) loglar.
        /// </summary>
        /// <param name="context">Mevcut DbContext (RestoranContext)</param>
        /// <param name="eskiVeri">Silme ve güncellemede eski ürün verisi, eklemede null olabilir.</param>
        /// <param name="yeniVeri">Ekleme ve güncellemede yeni ürün verisi, silmede null olabilir.</param>
        /// <param name="tur">İşlem türü (Ekleme=0, Guncelleme=1, Silme=2)</param>
        public static void KayitEkle(RestoranContext context, Urun eskiVeri, Urun yeniVeri, int tur)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Ürün referansı (silmede eski, eklemede/güncellemede yeni)
            var urun = yeniVeri ?? eskiVeri;
            if (urun == null)
                return;

            var log = new UrunHareketleri
            {
                SatisKodu = "LOG", // Log kaydı olduğunu belirtmek için
                UrunId = urun.Id,
                Miktari = 0, // Log kaydı için miktar kullanılmıyor
                BirimMiktarı = 0,
                BirimFiyati = 0, // Log kaydı için fiyat kullanılmıyor
                Aciklama = BuildAciklama(eskiVeri, yeniVeri, tur),
                Tarih = DateTime.Now
            };

            context.Set<UrunHareketleri>().Add(log);
            // Not: SaveChanges dışarıdan (buton içinden) bir kere çağrılacak.
        }

        /// <summary>
        /// İşlem türüne göre detaylı açıklama metni oluşturur.
        /// </summary>
        private static string BuildAciklama(Urun eski, Urun yeni, int tur)
        {
            var culture = new CultureInfo("tr-TR");
            var sb = new StringBuilder();

            switch (tur)
            {
                case 0: // Ekleme
                    if (yeni != null)
                        sb.AppendFormat("Sisteme '{0}' ürünü eklendi.", yeni.UrunAdi);
                    break;

                case 1: // Silme
                    if (eski != null)
                        sb.AppendFormat("'{0}' ürünü silindi.", eski.UrunAdi);
                    break;

                case 2: // Güncelleme
                    if (eski == null || yeni == null)
                        break;

                    sb.AppendFormat("'{0}' ürünü güncellendi. ", eski.UrunAdi);

                    // Ürün Adı değişti mi?
                    if (!string.Equals(eski.UrunAdi ?? string.Empty, yeni.UrunAdi ?? string.Empty, StringComparison.Ordinal))
                    {
                        sb.AppendFormat("Ürün adı '{0}' iken '{1}' yapıldı. ",
                            eski.UrunAdi, yeni.UrunAdi);
                    }

                    // Ürün Kodu değişti mi?
                    if (!string.Equals(eski.UrunKodu ?? string.Empty, yeni.UrunKodu ?? string.Empty, StringComparison.Ordinal))
                    {
                        sb.AppendFormat("Ürün kodu '{0}' iken '{1}' yapıldı. ",
                            eski.UrunKodu, yeni.UrunKodu);
                    }

                    // Fiyat (müşteri fiyatı: BirimFiyati2) değişti mi?
                    if (eski.BirimFiyati2 != yeni.BirimFiyati2)
                    {
                        sb.AppendFormat("Fiyatı {0} TL'den {1} TL'ye değiştirildi. ",
                            eski.BirimFiyati2.ToString("N2", culture),
                            yeni.BirimFiyati2.ToString("N2", culture));
                    }

                    // BirimFiyati1 değişti mi?
                    if (eski.BirimFiyati1 != yeni.BirimFiyati1)
                    {
                        sb.AppendFormat("Birim Fiyat 1 {0} TL'den {1} TL'ye değiştirildi. ",
                            eski.BirimFiyati1.ToString("N2", culture),
                            yeni.BirimFiyati1.ToString("N2", culture));
                    }

                    // Açıklama değişti mi?
                    if (!string.Equals(eski.Aciklama ?? string.Empty,
                                       yeni.Aciklama ?? string.Empty,
                                       StringComparison.Ordinal))
                    {
                        sb.Append("Açıklama bilgisi güncellendi. ");
                    }

                    // Kategori (MenuId) değişti mi?
                    if (eski.MenuId != yeni.MenuId)
                    {
                        sb.AppendFormat("Ürün farklı bir menü kategorisine taşındı (MenuId {0} -> {1}). ",
                            eski.MenuId, yeni.MenuId);
                    }

                    if (sb.Length == 0)
                        sb.Append("Ürün üzerinde güncelleme yapıldı.");
                    break;
            }

            return sb.ToString().Trim();
        }
    }
}
