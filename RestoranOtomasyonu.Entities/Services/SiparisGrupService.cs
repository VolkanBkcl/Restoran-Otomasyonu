using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.DTOs;
using RestoranOtomasyonu.Entities.Enums;
using RestoranOtomasyonu.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestoranOtomasyonu.Entities.Services
{
    // Sipariş gruplandırma servisi
    // Aynı masa ve yakın zamanda verilen siparişleri gruplar
    
    public class SiparisGrupService
    {
        private readonly SiparislerDal _siparislerDal;
        private readonly MasaHareketleriDal _masaHareketleriDal;
        private readonly UrunDal _urunDal;
        private readonly MasalarDal _masalarDal;
        private readonly KullanicilarDal _kullanicilarDal;

        public SiparisGrupService()
        {
            _siparislerDal = new SiparislerDal();
            _masaHareketleriDal = new MasaHareketleriDal();
            _urunDal = new UrunDal();
            _masalarDal = new MasalarDal();
            _kullanicilarDal = new KullanicilarDal();
        }


        // Tüm siparişleri gruplandırılmış olarak getir

        public List<SiparisGrupDTO> GetGruplandirilmisSiparisler(RestoranContext context, int? masaId = null)
        {
            // Siparişleri getir
            var siparisler = _siparislerDal.GetAll(context, s => 
                s.SiparisDurumu != SiparisDurumu.IptalEdildi && 
                (masaId == null || s.MasaId == masaId.Value))
                .OrderByDescending(s => s.Tarih)
                .ToList();

            // Gruplandır (aynı masa ve aynı dakika içindeki siparişler)
            var gruplar = new Dictionary<string, SiparisGrupDTO>();

            foreach (var siparis in siparisler)
            {
                // Grup ID oluştur: MasaId + Tarih (dakika bazlı)
                var grupTarih = new DateTime(
                    siparis.Tarih.Year,
                    siparis.Tarih.Month,
                    siparis.Tarih.Day,
                    siparis.Tarih.Hour,
                    siparis.Tarih.Minute,
                    0); // Saniye ve milisaniye sıfırlanır

                var grupId = $"{siparis.MasaId}_{grupTarih:yyyyMMddHHmm}";

                if (!gruplar.ContainsKey(grupId))
                {
                    // Yeni grup oluştur
                    var masa = _masalarDal.GetByFilter(context, m => m.Id == siparis.MasaId);
                    var grup = new SiparisGrupDTO
                    {
                        GrupId = grupId,
                        MasaId = siparis.MasaId,
                        MasaAdi = masa?.MasaAdi ?? $"Masa {siparis.MasaId}",
                        IlkSiparisTarihi = siparis.Tarih,
                        SonSiparisTarihi = siparis.Tarih,
                        Durum = siparis.SiparisDurumu,
                        DurumMetni = GetDurumMetni(siparis.SiparisDurumu),
                        ToplamTutar = 0,
                        NetTutar = 0,
                        SiparisSayisi = 0,
                        KullaniciSayisi = 0,
                        SiparisDetaylari = new List<SiparisDetayDTO>()
                    };
                    gruplar[grupId] = grup;
                }

                var mevcutGrup = gruplar[grupId];

                // Sipariş detayını oluştur
                var kullanici = _kullanicilarDal.GetByFilter(context, k => k.Id == siparis.KullaniciId);
                var siparisDetay = new SiparisDetayDTO
                {
                    SiparisId = siparis.Id,
                    KullaniciId = siparis.KullaniciId,
                    KullaniciAdi = kullanici?.KullaniciAdi ?? "",
                    AdSoyad = kullanici?.AdSoyad ?? "",
                    SatisKodu = siparis.SatisKodu ?? "",
                    Tutar = siparis.Tutar,
                    NetTutar = siparis.NetTutar,
                    Durum = siparis.SiparisDurumu,
                    DurumMetni = GetDurumMetni(siparis.SiparisDurumu),
                    Tarih = siparis.Tarih,
                    Aciklama = siparis.Aciklama ?? "",
                    Urunler = new List<SiparisUrunDTO>()
                };

                // Bu siparişe ait ürünleri getir (MasaHareketleri'nden)
                var masaHareketleri = _masaHareketleriDal.GetAll(context, 
                    mh => mh.SatisKodu == siparis.SatisKodu && mh.UrunId > 0)
                    .ToList();

                foreach (var hareket in masaHareketleri)
                {
                    var urun = _urunDal.GetByFilter(context, u => u.Id == hareket.UrunId);
                    if (urun != null)
                    {
                        siparisDetay.Urunler.Add(new SiparisUrunDTO
                        {
                            UrunId = urun.Id,
                            UrunAdi = urun.UrunAdi ?? "",
                            Miktar = hareket.Miktari,
                            BirimFiyat = hareket.BirimFiyati,
                            ToplamFiyat = hareket.BirimFiyati * hareket.Miktari,
                            Aciklama = hareket.Aciklama ?? ""
                        });
                    }
                }

                // Gruba ekle
                mevcutGrup.SiparisDetaylari.Add(siparisDetay);
                mevcutGrup.ToplamTutar += siparis.Tutar;
                mevcutGrup.NetTutar += siparis.NetTutar;
                mevcutGrup.SiparisSayisi++;

                // Tarih güncellemeleri
                if (siparis.Tarih < mevcutGrup.IlkSiparisTarihi)
                    mevcutGrup.IlkSiparisTarihi = siparis.Tarih;
                if (siparis.Tarih > mevcutGrup.SonSiparisTarihi)
                    mevcutGrup.SonSiparisTarihi = siparis.Tarih;

                // Durum güncellemesi (en yüksek öncelikli durum)
                if (siparis.SiparisDurumu < mevcutGrup.Durum || 
                    (siparis.SiparisDurumu == SiparisDurumu.Hazirlaniyor && mevcutGrup.Durum == SiparisDurumu.SiparisAlindi))
                {
                    mevcutGrup.Durum = siparis.SiparisDurumu;
                    mevcutGrup.DurumMetni = GetDurumMetni(siparis.SiparisDurumu);
                }
            }

            // Kullanıcı sayısını hesapla
            foreach (var grup in gruplar.Values)
            {
                grup.KullaniciSayisi = grup.SiparisDetaylari.Select(s => s.KullaniciId).Distinct().Count();
            }

            return gruplar.Values.OrderByDescending(g => g.SonSiparisTarihi).ToList();
        }


        /// Sipariş durumunu güncelle

        public bool SiparisDurumuGuncelle(RestoranContext context, int siparisId, SiparisDurumu yeniDurum)
        {
            var siparis = _siparislerDal.GetByFilter(context, s => s.Id == siparisId);
            if (siparis == null)
                return false;

            siparis.SiparisDurumu = yeniDurum;
            return _siparislerDal.AddOrUpdate(context, siparis);
        }

    
        /// Grup içindeki tüm siparişlerin durumunu güncelle

        public bool GrupDurumuGuncelle(RestoranContext context, string grupId, SiparisDurumu yeniDurum)
        {
            // Grup ID'den MasaId ve Tarih bilgisini çıkar
            var parts = grupId.Split('_');
            if (parts.Length != 2)
                return false;

            if (!int.TryParse(parts[0], out int masaId))
                return false;

            // Tarih formatı: yyyyMMddHHmm
            if (parts[1].Length != 12)
                return false;

            var yil = int.Parse(parts[1].Substring(0, 4));
            var ay = int.Parse(parts[1].Substring(4, 2));
            var gun = int.Parse(parts[1].Substring(6, 2));
            var saat = int.Parse(parts[1].Substring(8, 2));
            var dakika = int.Parse(parts[1].Substring(10, 2));

            var grupBaslangic = new DateTime(yil, ay, gun, saat, dakika, 0);
            var grupBitis = grupBaslangic.AddMinutes(1);

            // Bu zaman aralığındaki tüm siparişleri bul
            var siparisler = _siparislerDal.GetAll(context, s => 
                s.MasaId == masaId && 
                s.Tarih >= grupBaslangic && 
                s.Tarih < grupBitis &&
                s.SiparisDurumu != SiparisDurumu.IptalEdildi)
                .ToList();

            bool basarili = true;
            foreach (var siparis in siparisler)
            {
                siparis.SiparisDurumu = yeniDurum;
                if (!_siparislerDal.AddOrUpdate(context, siparis))
                    basarili = false;
            }

            return basarili;
        }


        /// Durum metnini Türkçe olarak döndür
 
        private string GetDurumMetni(SiparisDurumu durum)
        {
            // Enum değerine göre kontrol et (aynı değere sahip enum'lar için tek case kullan)
            int durumDegeri = (int)durum;
            
            switch (durumDegeri)
            {
                case 0: // SiparisAlindi veya OnayBekliyor
                    return "Sipariş Alındı";
                case 1: // Hazirlaniyor
                    return "Hazırlanıyor";
                case 2: // Hazir veya TeslimEdildi
                    return "Hazır";
                case 3: // ServisEdildi veya Tamamlandi
                    return "Servis Edildi";
                case 4: // IptalEdildi
                    return "İptal Edildi";
                default:
                    return "Bilinmeyen";
            }
        }
    }
}
