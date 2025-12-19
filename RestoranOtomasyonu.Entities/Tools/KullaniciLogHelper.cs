using RestoranOtomasyonu.Entities.Models;

using System;

using System.Text;



namespace RestoranOtomasyonu.Entities.Tools

{

    /// <summary>

    /// Kullanıcı (üyeler) üzerinde yapılan işlemleri KullaniciHareketleri tablosuna loglayan yardımcı sınıf.

    /// </summary>

    public static class KullaniciLogHelper

    {

        /// <summary>

        /// Kullanıcı ekleme / güncelleme / silme işlemini loglar.

        /// </summary>

        /// <param name="context">Mevcut DbContext (RestoranContext)</param>

        /// <param name="eskiVeri">Silme ve güncellemede eski kullanıcı verisi, eklemede null olabilir.</param>

        /// <param name="yeniVeri">Ekleme ve güncellemede yeni kullanıcı verisi, silmede null olabilir.</param>

        /// <param name="tur">İşlem türü (0=Ekleme, 1=Silme, 2=Güncelleme)</param>

        public static void KayitEkle(RestoranContext context, Kullanicilar eskiVeri, Kullanicilar yeniVeri, int tur)

        {

            if (context == null)

                throw new ArgumentNullException(nameof(context));



            var kullanici = yeniVeri ?? eskiVeri;

            if (kullanici == null)

                return;



            var log = new KullaniciHareketleri

            {

                KullaniciId = kullanici.Id,

                Aciklama = BuildAciklama(eskiVeri, yeniVeri, tur),

                Tarih = DateTime.Now

            };



            context.Set<KullaniciHareketleri>().Add(log);

            // SaveChanges, çağıran metotta bir kere yapılacak.

        }



        private static string BuildAciklama(Kullanicilar eski, Kullanicilar yeni, int tur)

        {

            var sb = new StringBuilder();



            switch (tur)

            {

                case 0: // Ekleme

                    if (yeni != null)

                        sb.AppendFormat("Sisteme yeni üye eklendi: {0} (Kullanıcı Adı: {1}).", yeni.AdSoyad, yeni.KullaniciAdi);

                    break;



                case 1: // Silme

                    if (eski != null)

                        sb.AppendFormat("Üye silindi: {0} (Kullanıcı Adı: {1}).", eski.AdSoyad, eski.KullaniciAdi);

                    break;



                case 2: // Güncelleme

                    if (eski == null || yeni == null)

                        break;



                    sb.AppendFormat("'{0}' kullanıcısı güncellendi. ", eski.AdSoyad);



                    // AdSoyad değişti mi?

                    if (!string.Equals(eski.AdSoyad ?? string.Empty, yeni.AdSoyad ?? string.Empty, StringComparison.Ordinal))

                    {

                        sb.AppendFormat("Adı Soyadı '{0}' iken '{1}' yapıldı. ", eski.AdSoyad, yeni.AdSoyad);

                    }



                    // Telefon değişti mi?

                    if (!string.Equals(eski.Telefon ?? string.Empty, yeni.Telefon ?? string.Empty, StringComparison.Ordinal))

                    {

                        sb.AppendFormat("Telefon numarası '{0}' iken '{1}' yapıldı. ", eski.Telefon, yeni.Telefon);

                    }



                    // Adres değişti mi?

                    if (!string.Equals(eski.Adres ?? string.Empty, yeni.Adres ?? string.Empty, StringComparison.Ordinal))

                    {

                        sb.Append("Adres bilgisi güncellendi. ");

                    }



                    // Email değişti mi?

                    if (!string.Equals(eski.Email ?? string.Empty, yeni.Email ?? string.Empty, StringComparison.OrdinalIgnoreCase))

                    {

                        sb.AppendFormat("E-posta '{0}' iken '{1}' yapıldı. ", eski.Email, yeni.Email);

                    }



                    // Görev değişti mi?

                    if (!string.Equals(eski.Gorevi ?? string.Empty, yeni.Gorevi ?? string.Empty, StringComparison.Ordinal))

                    {

                        sb.AppendFormat("Görevi '{0}' iken '{1}' yapıldı. ", eski.Gorevi, yeni.Gorevi);

                    }



                    // Kullanıcı adı değişti mi?

                    if (!string.Equals(eski.KullaniciAdi ?? string.Empty, yeni.KullaniciAdi ?? string.Empty, StringComparison.Ordinal))

                    {

                        sb.AppendFormat("Kullanıcı adı '{0}' iken '{1}' yapıldı. ", eski.KullaniciAdi, yeni.KullaniciAdi);

                    }



                    // Parola değişti mi? (eski/yeni parolayı göstermeden)

                    if (!string.Equals(eski.Parola ?? string.Empty, yeni.Parola ?? string.Empty, StringComparison.Ordinal))

                    {

                        sb.Append("Parola bilgisi güncellendi. ");

                    }



                    // AktifMi değişti mi?

                    if (eski.AktifMi != yeni.AktifMi)

                    {

                        sb.AppendFormat("Kullanıcı durumu '{0}' iken '{1}' yapıldı. ",

                            eski.AktifMi ? "Aktif" : "Pasif",

                            yeni.AktifMi ? "Aktif" : "Pasif");

                    }



                    // Açıklama değişti mi?

                    if (!string.Equals(eski.Aciklama ?? string.Empty, yeni.Aciklama ?? string.Empty, StringComparison.Ordinal))

                    {

                        sb.Append("Açıklama alanı güncellendi. ");

                    }



                    if (sb.Length == 0)

                        sb.Append("Kullanıcı üzerinde güncelleme yapıldı.");

                    break;

            }



            return sb.ToString().Trim();

        }

    }

}

