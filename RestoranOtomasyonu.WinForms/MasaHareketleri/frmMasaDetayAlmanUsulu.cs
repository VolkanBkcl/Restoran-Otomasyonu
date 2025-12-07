using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.MasaHareketleri
{
    /// <summary>
    /// Alman Usulü (Parçalı Ödeme) Masa Detay Formu
    /// Kullanıcı bazlı gruplama ile kimin ne yediği ve ne kadar ödediği gösterilir
    /// </summary>
    public partial class frmMasaDetayAlmanUsulu : XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private SiparislerDal siparislerDal = new SiparislerDal();
        private int _masaId;

        public frmMasaDetayAlmanUsulu(int masaId)
        {
            InitializeComponent();
            _masaId = masaId;
            this.Text = $"Masa {masaId} - Alman Usulü Detay";
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Siparisler tablosundan masaya ait siparişleri getir (Kullanıcı bilgileri ile)
                var siparisler = siparislerDal.GetAll(context)
                    .Where(s => s.MasaId == _masaId)
                    .ToList();

                // Kullanıcı bazlı gruplama
                var kullaniciGruplari = siparisler
                    .GroupBy(s => new { s.KullaniciId, s.Kullanicilar.AdSoyad, s.Kullanicilar.KullaniciAdi })
                    .Select(g => new
                    {
                        KullaniciId = g.Key.KullaniciId,
                        KullaniciAdi = g.Key.KullaniciAdi,
                        AdSoyad = g.Key.AdSoyad ?? g.Key.KullaniciAdi,
                        Siparisler = g.ToList(),
                        ToplamTutar = g.Sum(s => s.NetTutar),
                        OdenenTutar = g.Where(s => s.OdemeDurumu != Entities.Enums.OdemeDurumu.Odenmedi)
                                      .Sum(s => s.NetTutar),
                        KalanTutar = g.Where(s => s.OdemeDurumu == Entities.Enums.OdemeDurumu.Odenmedi)
                                      .Sum(s => s.NetTutar)
                    })
                    .OrderBy(g => g.AdSoyad)
                    .ToList();

                // Grid'e bağla
                gridControlAlmanUsulu.DataSource = kullaniciGruplari;
                gridViewAlmanUsulu.BestFitColumns();

                // Toplam bilgileri göster
                decimal genelToplam = kullaniciGruplari.Sum(g => g.ToplamTutar);
                decimal genelOdenen = kullaniciGruplari.Sum(g => g.OdenenTutar);
                decimal genelKalan = kullaniciGruplari.Sum(g => g.KalanTutar);

                lblGenelToplam.Text = $"Genel Toplam: {genelToplam:C2}";
                lblGenelOdenen.Text = $"Ödenen: {genelOdenen:C2}";
                lblGenelKalan.Text = $"Kalan: {genelKalan:C2}";

                // Kalan tutar rengi
                if (genelKalan > 0)
                    lblGenelKalan.Appearance.ForeColor = System.Drawing.Color.Red;
                else
                    lblGenelKalan.Appearance.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Veri yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

