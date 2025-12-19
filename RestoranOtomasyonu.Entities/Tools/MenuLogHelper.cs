using RestoranOtomasyonu.Entities.Models;
using System;
using System.Globalization;
using System.Text;

namespace RestoranOtomasyonu.Entities.Tools
{
    /// <summary>
    /// Menu (Kategori) işlemlerini MenuHareketleri tablosuna log olarak kaydeden yardımcı sınıf.
    /// </summary>
    public static class MenuLogHelper
    {
        /// <summary>
        /// Menu üzerinde yapılan işlemi (ekleme / güncelleme / silme) loglar.
        /// </summary>
        /// <param name="context">Mevcut DbContext (RestoranContext)</param>
        /// <param name="eskiVeri">Silme ve güncellemede eski menu verisi, eklemede null olabilir.</param>
        /// <param name="yeniVeri">Ekleme ve güncellemede yeni menu verisi, silmede null olabilir.</param>
        /// <param name="tur">İşlem türü (Ekleme=0, Guncelleme=1, Silme=2)</param>
        public static void KayitEkle(RestoranContext context, Menu eskiVeri, Menu yeniVeri, int tur)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Menu referansı (silmede eski, eklemede/güncellemede yeni)
            var menu = yeniVeri ?? eskiVeri;
            if (menu == null)
                return;

            var log = new MenuHareketleri
            {
                SatisKodu = "LOG", // Log kaydı olduğunu belirtmek için
                MenuId = menu.Id,
                Miktari = 0, // Log kaydı için miktar kullanılmıyor
                BirimMiktarı = 0,
                BirimFiyati = 0, // Log kaydı için fiyat kullanılmıyor
                Aciklama = BuildAciklama(eskiVeri, yeniVeri, tur),
                Tarih = DateTime.Now
            };

            context.Set<MenuHareketleri>().Add(log);
            // Not: SaveChanges dışarıdan (buton içinden) bir kere çağrılacak.
        }

        /// <summary>
        /// İşlem türüne göre detaylı açıklama metni oluşturur.
        /// </summary>
        private static string BuildAciklama(Menu eski, Menu yeni, int tur)
        {
            var sb = new StringBuilder();

            switch (tur)
            {
                case 0: // Ekleme
                    if (yeni != null)
                        sb.AppendFormat("Sisteme '{0}' menüsü eklendi.", yeni.MenuAdi);
                    break;

                case 1: // Silme
                    if (eski != null)
                        sb.AppendFormat("'{0}' menüsü silindi.", eski.MenuAdi);
                    break;

                case 2: // Güncelleme
                    if (eski == null || yeni == null)
                        break;

                    sb.AppendFormat("'{0}' menüsü güncellendi. ", eski.MenuAdi);

                    // Menu Adı değişti mi?
                    if (!string.Equals(eski.MenuAdi ?? string.Empty, yeni.MenuAdi ?? string.Empty, StringComparison.Ordinal))
                    {
                        sb.AppendFormat("Menü adı '{0}' iken '{1}' yapıldı. ",
                            eski.MenuAdi, yeni.MenuAdi);
                    }

                    // Açıklama değişti mi?
                    if (!string.Equals(eski.Aciklama ?? string.Empty,
                                       yeni.Aciklama ?? string.Empty,
                                       StringComparison.Ordinal))
                    {
                        sb.Append("Açıklama bilgisi güncellendi. ");
                    }

                    if (sb.Length == 0)
                        sb.Append("Menü üzerinde güncelleme yapıldı.");
                    break;
            }

            return sb.ToString().Trim();
        }
    }
}
