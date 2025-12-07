using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Models;
using KullanicilarEntity = RestoranOtomasyonu.Entities.Models.Kullanicilar;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Login
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private KullanicilarDal kullanicilarDal = new KullanicilarDal();
        private RestoranContext context = new RestoranContext();
        private KullanicilarEntity girisYapanKullanici;

        public KullanicilarEntity GirisYapanKullanici => girisYapanKullanici;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // NavigationFrame'i Giriş ekranına yönlendir
            navigationFrame1.SelectedPageIndex = 0;
            
            // "Beni Hatırla" checkbox'ı için önceki kaydı kontrol et (Registry veya Settings kullanılabilir)
            // Şimdilik boş bırakıldı, gerekirse eklenebilir
        }

        #region GİRİŞ EKRANI

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtParola.Text))
            {
                XtraMessageBox.Show("Lütfen kullanıcı adı ve parola giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kullanıcı kontrolü
            var kullanici = kullanicilarDal.GetByFilter(context, 
                k => k.KullaniciAdi == txtKullaniciAdi.Text && k.Parola == txtParola.Text);

            if (kullanici == null)
            {
                XtraMessageBox.Show("Kullanıcı adı veya parola hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Aktif kullanıcı kontrolü
            if (!kullanici.AktifMi)
            {
                XtraMessageBox.Show("Bu kullanıcı hesabı pasif durumda. Lütfen yönetici ile iletişime geçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Başarılı giriş
            girisYapanKullanici = kullanici;

            // "Beni Hatırla" ayarlarını kaydet (Registry veya Settings kullanılabilir)
            // Şimdilik boş bırakıldı, gerekirse eklenebilir

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void hyperlinkLabelControlKaydol_Click(object sender, EventArgs e)
        {
            // Kayıt ekranına geç
            navigationFrame1.SelectedPageIndex = 1;
        }

        private void hyperlinkLabelControlSifremiUnuttum_Click(object sender, EventArgs e)
        {
            // Şifre kurtarma ekranına geç
            navigationFrame1.SelectedPageIndex = 2;
        }

        #endregion

        #region KAYIT EKRANI

        private void btnKaydol_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrWhiteSpace(txtKayitAdSoyad.Text))
            {
                XtraMessageBox.Show("Lütfen ad soyad giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKayitAdSoyad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKayitKullaniciAdi.Text))
            {
                XtraMessageBox.Show("Lütfen kullanıcı adı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKayitKullaniciAdi.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKayitParola.Text))
            {
                XtraMessageBox.Show("Lütfen parola giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKayitParola.Focus();
                return;
            }

            if (txtKayitParola.Text != txtKayitParolaTekrar.Text)
            {
                XtraMessageBox.Show("Parolalar eşleşmiyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKayitParolaTekrar.Focus();
                return;
            }

            // Kullanıcı adı kontrolü
            var mevcutKullanici = kullanicilarDal.GetByFilter(context, 
                k => k.KullaniciAdi == txtKayitKullaniciAdi.Text);
            
            if (mevcutKullanici != null)
            {
                XtraMessageBox.Show("Bu kullanıcı adı zaten kullanılıyor. Lütfen farklı bir kullanıcı adı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKayitKullaniciAdi.Focus();
                return;
            }

            // Yeni kullanıcı oluştur
            var yeniKullanici = new KullanicilarEntity
            {
                AdSoyad = txtKayitAdSoyad.Text,
                Telefon = txtKayitTelefon.Text,
                Email = txtKayitEmail.Text,
                KullaniciAdi = txtKayitKullaniciAdi.Text,
                Parola = txtKayitParola.Text,
                HatirlatmaSorusu = txtKayitHatirlatmaSorusu.Text,
                Cevap = txtKayitCevap.Text,
                Gorevi = "Musteri", // Otomatik set
                KayitTarihi = DateTime.Now, // Otomatik set
                AktifMi = true // Otomatik set
            };

            // Veritabanına kaydet
            if (kullanicilarDal.AddOrUpdate(context, yeniKullanici))
            {
                kullanicilarDal.Save(context);
                XtraMessageBox.Show("Kayıt başarıyla tamamlandı! Giriş yapabilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Giriş ekranına dön ve kullanıcı adını doldur
                navigationFrame1.SelectedPageIndex = 0;
                txtKullaniciAdi.Text = txtKayitKullaniciAdi.Text;
                txtParola.Text = string.Empty;
                
                // Kayıt formunu temizle
                TemizleKayitFormu();
            }
            else
            {
                XtraMessageBox.Show("Kayıt sırasında bir hata oluştu. Lütfen bilgilerinizi kontrol ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void hyperlinkLabelControlGeriDon_Click(object sender, EventArgs e)
        {
            // Giriş ekranına dön
            navigationFrame1.SelectedPageIndex = 0;
            TemizleKayitFormu();
        }

        private void TemizleKayitFormu()
        {
            txtKayitAdSoyad.Text = string.Empty;
            txtKayitTelefon.Text = string.Empty;
            txtKayitEmail.Text = string.Empty;
            txtKayitKullaniciAdi.Text = string.Empty;
            txtKayitParola.Text = string.Empty;
            txtKayitParolaTekrar.Text = string.Empty;
            txtKayitHatirlatmaSorusu.Text = string.Empty;
            txtKayitCevap.Text = string.Empty;
        }

        #endregion

        #region ŞİFRE KURTARMA EKRANI

        private void btnKullaniciAdiKontrol_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSifreKurtarmaKullaniciAdi.Text))
            {
                XtraMessageBox.Show("Lütfen kullanıcı adınızı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kullanici = kullanicilarDal.GetByFilter(context, 
                k => k.KullaniciAdi == txtSifreKurtarmaKullaniciAdi.Text);

            if (kullanici == null)
            {
                XtraMessageBox.Show("Bu kullanıcı adı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hatırlatma sorusunu göster
            lblHatirlatmaSorusu.Text = kullanici.HatirlatmaSorusu;
            lblHatirlatmaSorusu.Visible = true;
            txtSifreKurtarmaCevap.Visible = true;
            txtSifreKurtarmaCevap.Enabled = true;
            btnCevapKontrol.Enabled = true;
            
            // Kullanıcıyı sakla (cevap kontrolü için)
            txtSifreKurtarmaKullaniciAdi.Tag = kullanici;
        }

        private void btnCevapKontrol_Click(object sender, EventArgs e)
        {
            if (txtSifreKurtarmaKullaniciAdi.Tag == null)
            {
                XtraMessageBox.Show("Önce kullanıcı adınızı kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kullanici = txtSifreKurtarmaKullaniciAdi.Tag as KullanicilarEntity;

            if (kullanici.Cevap != txtSifreKurtarmaCevap.Text)
            {
                XtraMessageBox.Show("Cevap hatalı! Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSifreKurtarmaCevap.Focus();
                return;
            }

            // Cevap doğru - yeni şifre belirleme alanlarını göster
            lblYeniParola.Visible = true;
            txtYeniParola.Visible = true;
            txtYeniParola.Enabled = true;
            lblYeniParolaTekrar.Visible = true;
            txtYeniParolaTekrar.Visible = true;
            txtYeniParolaTekrar.Enabled = true;
            btnSifreGuncelle.Enabled = true;
        }

        private void btnSifreGuncelle_Click(object sender, EventArgs e)
        {
            if (txtSifreKurtarmaKullaniciAdi.Tag == null)
            {
                return;
            }

            var kullanici = txtSifreKurtarmaKullaniciAdi.Tag as KullanicilarEntity;

            if (string.IsNullOrWhiteSpace(txtYeniParola.Text))
            {
                XtraMessageBox.Show("Lütfen yeni parola giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYeniParola.Focus();
                return;
            }

            if (txtYeniParola.Text != txtYeniParolaTekrar.Text)
            {
                XtraMessageBox.Show("Parolalar eşleşmiyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYeniParolaTekrar.Focus();
                return;
            }

            // Şifreyi güncelle
            kullanici.Parola = txtYeniParola.Text;

            if (kullanicilarDal.AddOrUpdate(context, kullanici))
            {
                kullanicilarDal.Save(context);
                XtraMessageBox.Show("Şifreniz başarıyla güncellendi! Giriş yapabilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Giriş ekranına dön
                navigationFrame1.SelectedPageIndex = 0;
                txtKullaniciAdi.Text = kullanici.KullaniciAdi;
                txtParola.Text = string.Empty;
                
                // Şifre kurtarma formunu temizle
                TemizleSifreKurtarmaFormu();
            }
            else
            {
                XtraMessageBox.Show("Şifre güncelleme sırasında bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void hyperlinkLabelControlGeriDonSifre_Click(object sender, EventArgs e)
        {
            // Giriş ekranına dön
            navigationFrame1.SelectedPageIndex = 0;
            TemizleSifreKurtarmaFormu();
        }

        private void TemizleSifreKurtarmaFormu()
        {
            txtSifreKurtarmaKullaniciAdi.Text = string.Empty;
            txtSifreKurtarmaKullaniciAdi.Tag = null;
            lblHatirlatmaSorusu.Text = string.Empty;
            lblHatirlatmaSorusu.Visible = false;
            txtSifreKurtarmaCevap.Text = string.Empty;
            txtSifreKurtarmaCevap.Visible = false;
            txtSifreKurtarmaCevap.Enabled = false;
            btnCevapKontrol.Enabled = false;
            lblYeniParola.Visible = false;
            txtYeniParola.Text = string.Empty;
            txtYeniParola.Visible = false;
            txtYeniParola.Enabled = false;
            lblYeniParolaTekrar.Visible = false;
            txtYeniParolaTekrar.Text = string.Empty;
            txtYeniParolaTekrar.Visible = false;
            txtYeniParolaTekrar.Enabled = false;
            btnSifreGuncelle.Enabled = false;
        }

        #endregion
    }
}

