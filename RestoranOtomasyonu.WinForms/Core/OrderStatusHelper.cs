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
                var siparis = context.Set<Siparisler>()
                    .FirstOrDefault(s => s.SatisKodu == satisKodu);

                if (siparis == null)
                    return;


                switch (newStatus)
                {
                    case OrderStatus.Bos:

                        siparis.SiparisDurumu = SiparisDurumu.Tamamlandi;
                        siparis.OdemeDurumu = OdemeDurumu.TumuOdendi;
                        break;

                    case OrderStatus.SiparisAlindi:
                        siparis.SiparisDurumu = SiparisDurumu.OnayBekliyor;
                        siparis.OdemeDurumu = OdemeDurumu.OdemeBekliyor;
                        break;

                    case OrderStatus.Hazirlaniyor:
                        siparis.SiparisDurumu = SiparisDurumu.Hazirlaniyor;

                        siparis.OdemeDurumu = OdemeDurumu.OdemeBekliyor;
                        break;

                    case OrderStatus.ServisEdildi:
                        siparis.SiparisDurumu = SiparisDurumu.TeslimEdildi;
                        siparis.OdemeDurumu = OdemeDurumu.OdemeBekliyor;
                        break;

                    case OrderStatus.OdemeBekleniyor:

                        siparis.SiparisDurumu = SiparisDurumu.TeslimEdildi;
                        siparis.OdemeDurumu = OdemeDurumu.OdemeBekliyor;
                        break;

                    case OrderStatus.Odendi:

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

                    return (Color.Gold, Color.Black);

                case OrderStatus.Hazirlaniyor:

                    return (Color.Orange, Color.Black);

                case OrderStatus.ServisEdildi:

                    return (Color.SteelBlue, Color.White);

                case OrderStatus.OdemeBekleniyor:

                    return (Color.Firebrick, Color.White);

                case OrderStatus.Odendi:

                    return (Color.SeaGreen, Color.White);

                case OrderStatus.Bos:
                default:

                    return (Color.LightGray, Color.Black);
            }
        }
    }
}

