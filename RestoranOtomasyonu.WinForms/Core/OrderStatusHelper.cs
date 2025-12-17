using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.Enums;
using RestoranOtomasyonu.Entities.Models;

namespace RestoranOtomasyonu.WinForms.Core
{
    /// <summary>
    /// Sipariş durumu yönetimi için yardımcı (helper) sınıf.
    /// - Veritabanındaki <see cref="Siparisler"/> kaydının durumunu günceller.
    /// - Masa butonlarını (Button / SimpleButton) verilen duruma göre renklendirir.
    /// </summary>
    public static class OrderStatusHelper
    {
        /// <summary>
        /// Veritabanındaki siparişin durumunu, satış koduna göre günceller.
        /// </summary>
        /// <param name="satisKodu">Sipariş ile ilişkilendirilmiş satış kodu.</param>
        /// <param name="newStatus">Yeni durum (OrderStatus).</param>
        public static void UpdateOrderStatus(string satisKodu, OrderStatus newStatus)
        {
            if (string.IsNullOrWhiteSpace(satisKodu))
                return;

            using (var context = new RestoranContext())
            {
                // Aynı satış koduna sahip birden fazla sipariş olma ihtimali düşük
                // ancak yine de güvenli tarafta kalmak için FirstOrDefault ile tek kayıt alıyoruz.
                var siparis = context.Set<Siparisler>()
                    .FirstOrDefault(s => s.SatisKodu == satisKodu);

                if (siparis == null)
                    return;

                // OrderStatus -> SiparisDurumu / OdemeDurumu eşlemesi
                switch (newStatus)
                {
                    case OrderStatus.Bos:
                        // Masada aktif sipariş yok - tamamlanmış ve ödenmiş kabul ediyoruz
                        siparis.SiparisDurumu = SiparisDurumu.Tamamlandi;
                        siparis.OdemeDurumu = OdemeDurumu.TumuOdendi;
                        break;

                    case OrderStatus.SiparisAlindi:
                        siparis.SiparisDurumu = SiparisDurumu.OnayBekliyor;
                        siparis.OdemeDurumu = OdemeDurumu.OdemeBekliyor;
                        break;

                    case OrderStatus.Hazirlaniyor:
                        siparis.SiparisDurumu = SiparisDurumu.Hazirlaniyor;
                        // Ödeme hâlâ bekleniyor
                        siparis.OdemeDurumu = OdemeDurumu.OdemeBekliyor;
                        break;

                    case OrderStatus.ServisEdildi:
                        siparis.SiparisDurumu = SiparisDurumu.TeslimEdildi;
                        siparis.OdemeDurumu = OdemeDurumu.OdemeBekliyor;
                        break;

                    case OrderStatus.OdemeBekleniyor:
                        // Servis edilmiş ve hesap istenmiş
                        siparis.SiparisDurumu = SiparisDurumu.TeslimEdildi;
                        siparis.OdemeDurumu = OdemeDurumu.OdemeBekliyor;
                        break;

                    case OrderStatus.Odendi:
                        // Ödeme tamamlandı (örn. blockchain sonrası)
                        siparis.SiparisDurumu = SiparisDurumu.Tamamlandi;
                        siparis.OdemeDurumu = OdemeDurumu.TumuOdendi;
                        break;
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Masa butonunun (DevExpress SimpleButton) arka plan rengini,
        /// verilen <see cref="OrderStatus"/> değerine göre günceller.
        /// </summary>
        /// <param name="button">Renklendirilecek masa butonu.</param>
        /// <param name="status">Masanın/siparişin güncel durumu.</param>
        public static void RefreshTableStatus(SimpleButton button, OrderStatus status)
        {
            if (button == null)
                return;

            var colors = GetStatusColors(status);

            button.Appearance.BackColor = colors.background;
            button.Appearance.ForeColor = colors.foreground;
            button.Appearance.Options.UseBackColor = true;
            button.Appearance.Options.UseForeColor = true;

            // İsteğe bağlı: buton text'ine durumu eklemek için aşağıdaki satırı aktif edebilirsiniz.
            // button.Text = $"{button.Text.Split(new[] { '-' }, 2)[0].Trim()} - {status}";
        }

        /// <summary>
        /// Standart WinForms <see cref="Button"/> kontrolü için
        /// masa durumuna göre renklendirme yapar.
        /// </summary>
        /// <param name="button">Renklendirilecek Windows Forms butonu.</param>
        /// <param name="status">Masanın/siparişin güncel durumu.</param>
        public static void RefreshTableStatus(Button button, OrderStatus status)
        {
            if (button == null)
                return;

            var colors = GetStatusColors(status);

            button.BackColor = colors.background;
            button.ForeColor = colors.foreground;
        }

        /// <summary>
        /// Verilen durum için kullanılacak arka plan ve yazı rengini döner.
        /// </summary>
        private static (Color background, Color foreground) GetStatusColors(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.SiparisAlindi:
                    // Sarı
                    return (Color.Gold, Color.Black);

                case OrderStatus.Hazirlaniyor:
                    // Turuncu
                    return (Color.Orange, Color.Black);

                case OrderStatus.ServisEdildi:
                    // Mavi
                    return (Color.SteelBlue, Color.White);

                case OrderStatus.OdemeBekleniyor:
                    // Kırmızı
                    return (Color.Firebrick, Color.White);

                case OrderStatus.Odendi:
                    // Yeşil (özellikle blockchain sonrası)
                    return (Color.SeaGreen, Color.White);

                case OrderStatus.Bos:
                default:
                    // Boş masa için nötr gri
                    return (Color.LightGray, Color.Black);
            }
        }
    }
}

