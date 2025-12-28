using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using RestoranOtomasyonu.WinForms.Masalar;
using RestoranOtomasyonu.WinForms.MasaHareketleri;
using RestoranOtomasyonu.WinForms.Menular;
using RestoranOtomasyonu.WinForms.MenuHareketleri;
using RestoranOtomasyonu.WinForms.Urunler;
using RestoranOtomasyonu.WinForms.UrunHareketleri;
using RestoranOtomasyonu.WinForms.Kullanicilar;
using RestoranOtomasyonu.WinForms.KullaniciHareketleri;
using RestoranOtomasyonu.WinForms.Roller;
using RestoranOtomasyonu.WinForms.SiparisYonetim;
using RestoranOtomasyonu.WinForms.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.AnaMenu
{
    public partial class frmAnaMenu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        void FormGetir(XtraForm frm)
        {
            frm.MdiParent = this;
            frm.Show();
        }
        public frmAnaMenu()
        {
            InitializeComponent();
        }

        private void btnUrunler_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmUrunler frm = new frmUrunler();
            FormGetir(frm);
        }

        private void btnMenuler_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMenuler frm = new frmMenuler();
            FormGetir(frm);
        }

        private void frmAnaMenu_Load(object sender, EventArgs e)
        {
            // Modern görünüm ayarları
            this.ribbon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbonStatusBar.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            // Sipariş Yönetim butonunu sadece yetkili kullanıcılara göster
            // Yönetici, Garson veya Mutfak görevlileri görebilir
            bool siparisYonetimYetkisi = YetkiKontrolu.YoneticiMi || 
                                         YetkiKontrolu.GarsonMi || 
                                         YetkiKontrolu.RolVarMi("Mutfak");
            this.frmSiparisYonetim.Visibility = siparisYonetimYetkisi 
                ? DevExpress.XtraBars.BarItemVisibility.Always 
                : DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void btnMasalar_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMasalar frm = new frmMasalar();
            FormGetir(frm);
        }

        private void btnMasaHareketleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMasaHareketleri frm = new frmMasaHareketleri();
            FormGetir(frm);
        }

        private void btnKullanicilar_ItemClick(object sender, ItemClickEventArgs e)
        {
            // DEBUG: Buton tıklama öncesi bilgileri logla
            System.Diagnostics.Debug.WriteLine("=== btnKullanicilar TIKLAMA ===");
            System.Diagnostics.Debug.WriteLine($"MevcutKullanici null mu?: {YetkiKontrolu.MevcutKullanici == null}");
            if (YetkiKontrolu.MevcutKullanici != null)
            {
                System.Diagnostics.Debug.WriteLine($"Kullanıcı Görevi: '{YetkiKontrolu.MevcutKullanici.Gorevi}'");
                System.Diagnostics.Debug.WriteLine($"MevcutKullaniciGorevi: '{YetkiKontrolu.MevcutKullaniciGorevi}'");
                System.Diagnostics.Debug.WriteLine($"YoneticiMi: {YetkiKontrolu.YoneticiMi}");
            }
            System.Diagnostics.Debug.WriteLine("==============================");
            
            // Sadece Yönetici erişebilir
            if (!YetkiKontrolu.YoneticiMi)
            {
                XtraMessageBox.Show("Bu işlem için Yönetici yetkisi gereklidir.", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            frmKullanicilar frm = new frmKullanicilar();
            FormGetir(frm);
        }

        private void btnKullaniciHareketleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmKullaniciHareketleri frm = new frmKullaniciHareketleri();
            FormGetir(frm);
        }

        private void btnRoller_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmRoller frm = new frmRoller();
            FormGetir(frm);
        }

        private void btnMenuHareketleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMenuHareketleri frm = new frmMenuHareketleri();
            FormGetir(frm);
        }

        private void btnUrunHareketleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmUrunHareketleri frm = new frmUrunHareketleri();
            FormGetir(frm);
        }

        private void btnSiparisYonetim_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Sadece Yönetici, Garson veya Mutfak erişebilir
            if (!YetkiKontrolu.YoneticiMi && !YetkiKontrolu.GarsonMi && !YetkiKontrolu.RolVarMi("Mutfak"))
            {
                XtraMessageBox.Show("Bu işlem için Yönetici, Garson veya Mutfak yetkisi gereklidir.", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            frmSiparisYonetim frm = new frmSiparisYonetim();
            FormGetir(frm);
        }
    }
}