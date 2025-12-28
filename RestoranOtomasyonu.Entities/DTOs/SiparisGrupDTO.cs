using RestoranOtomasyonu.Entities.Enums;
using System;
using System.Collections.Generic;

namespace RestoranOtomasyonu.Entities.DTOs
{
    /// <summary>
    /// Gruplandırılmış sipariş DTO'su
    /// Aynı masa ve yakın zamanda verilen siparişleri tek grup olarak gösterir
    /// </summary>
    public class SiparisGrupDTO
    {
        /// <summary>
        /// Grup ID (MasaId + Tarih bazlı hash)
        /// </summary>
        public string GrupId { get; set; }

        /// <summary>
        /// Masa ID
        /// </summary>
        public int MasaId { get; set; }

        /// <summary>
        /// Masa Adı
        /// </summary>
        public string MasaAdi { get; set; }

        /// <summary>
        /// Grup içindeki siparişlerin toplam tutarı
        /// </summary>
        public decimal ToplamTutar { get; set; }

        /// <summary>
        /// Grup içindeki siparişlerin net tutarı (indirim sonrası)
        /// </summary>
        public decimal NetTutar { get; set; }

        /// <summary>
        /// Grup içindeki sipariş sayısı
        /// </summary>
        public int SiparisSayisi { get; set; }

        /// <summary>
        /// Grup içindeki kullanıcı sayısı
        /// </summary>
        public int KullaniciSayisi { get; set; }

        /// <summary>
        /// Sipariş durumu (Grup içindeki en yüksek öncelikli durum)
        /// </summary>
        public SiparisDurumu Durum { get; set; }

        /// <summary>
        /// Durum metni (Türkçe)
        /// </summary>
        public string DurumMetni { get; set; }

        /// <summary>
        /// İlk sipariş tarihi
        /// </summary>
        public DateTime IlkSiparisTarihi { get; set; }

        /// <summary>
        /// Son sipariş tarihi
        /// </summary>
        public DateTime SonSiparisTarihi { get; set; }

        /// <summary>
        /// Grup içindeki sipariş detayları
        /// </summary>
        public List<SiparisDetayDTO> SiparisDetaylari { get; set; }

        public SiparisGrupDTO()
        {
            SiparisDetaylari = new List<SiparisDetayDTO>();
        }
    }

    /// <summary>
    /// Sipariş detay DTO'su
    /// </summary>
    public class SiparisDetayDTO
    {
        /// <summary>
        /// Sipariş ID
        /// </summary>
        public int SiparisId { get; set; }

        /// <summary>
        /// Kullanıcı ID
        /// </summary>
        public int KullaniciId { get; set; }

        /// <summary>
        /// Kullanıcı Adı Soyadı
        /// </summary>
        public string KullaniciAdi { get; set; }

        /// <summary>
        /// Kullanıcı Ad Soyad
        /// </summary>
        public string AdSoyad { get; set; }

        /// <summary>
        /// Satış Kodu
        /// </summary>
        public string SatisKodu { get; set; }

        /// <summary>
        /// Sipariş tutarı
        /// </summary>
        public decimal Tutar { get; set; }

        /// <summary>
        /// Net tutar (indirim sonrası)
        /// </summary>
        public decimal NetTutar { get; set; }

        /// <summary>
        /// Sipariş durumu
        /// </summary>
        public SiparisDurumu Durum { get; set; }

        /// <summary>
        /// Durum metni
        /// </summary>
        public string DurumMetni { get; set; }

        /// <summary>
        /// Sipariş tarihi
        /// </summary>
        public DateTime Tarih { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Aciklama { get; set; }

        /// <summary>
        /// Bu siparişe ait ürünler (MasaHareketleri'nden)
        /// </summary>
        public List<SiparisUrunDTO> Urunler { get; set; }

        public SiparisDetayDTO()
        {
            Urunler = new List<SiparisUrunDTO>();
        }
    }

    /// <summary>
    /// Sipariş ürün DTO'su
    /// </summary>
    public class SiparisUrunDTO
    {
        /// <summary>
        /// Ürün ID
        /// </summary>
        public int UrunId { get; set; }

        /// <summary>
        /// Ürün Adı
        /// </summary>
        public string UrunAdi { get; set; }

        /// <summary>
        /// Miktar
        /// </summary>
        public int Miktar { get; set; }

        /// <summary>
        /// Birim Fiyat
        /// </summary>
        public decimal BirimFiyat { get; set; }

        /// <summary>
        /// Toplam Fiyat
        /// </summary>
        public decimal ToplamFiyat { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Aciklama { get; set; }
    }
}
