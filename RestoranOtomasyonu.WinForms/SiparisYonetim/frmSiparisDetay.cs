using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.SiparisYonetim
{
    public partial class frmSiparisDetay : DevExpress.XtraEditors.XtraForm
    {
        private SiparisGrupDTO grup;

        public frmSiparisDetay(SiparisGrupDTO grup)
        {
            InitializeComponent();
            this.grup = grup;
            VerileriYukle();
        }

        private void VerileriYukle()
        {
            // Grup bilgilerini göster
            lblMasaAdi.Text = $"Masa: {grup.MasaAdi}";
            lblToplamTutar.Text = $"Toplam Tutar: {grup.ToplamTutar:C}";
            lblNetTutar.Text = $"Net Tutar: {grup.NetTutar:C}";
            lblSiparisSayisi.Text = $"Sipariş Sayısı: {grup.SiparisSayisi}";
            lblKullaniciSayisi.Text = $"Kullanıcı Sayısı: {grup.KullaniciSayisi}";
            lblDurum.Text = $"Durum: {grup.DurumMetni}";
            lblTarih.Text = $"Tarih: {grup.IlkSiparisTarihi:dd.MM.yyyy HH:mm} - {grup.SonSiparisTarihi:HH:mm}";

            // Sipariş detaylarını göster
            var detayList = grup.SiparisDetaylari.Select(d => new
            {
                Kullanici = $"{d.AdSoyad} ({d.KullaniciAdi})",
                Tutar = d.NetTutar,
                Durum = d.DurumMetni,
                Tarih = d.Tarih.ToString("HH:mm"),
                UrunSayisi = d.Urunler.Count,
                Urunler = string.Join(", ", d.Urunler.Select(u => $"{u.UrunAdi} x{u.Miktar}"))
            }).ToList();

            gridControlDetaylar.DataSource = detayList;
            gridViewDetaylar.BestFitColumns();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
