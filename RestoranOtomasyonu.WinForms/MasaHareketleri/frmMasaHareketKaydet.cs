using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.WinForms.Core;
using RollerSabitleri = RestoranOtomasyonu.WinForms.Core.Roller;
using MasaHareketleriEntity = RestoranOtomasyonu.Entities.Models.MasaHareketleri;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.MasaHareketleri
{
    public partial class frmMasaHareketKaydet : DevExpress.XtraEditors.XtraForm
    {
        private MasaHareketleriDal masaHareketleriDal = new MasaHareketleriDal();
        private MasalarDal masalarDal = new MasalarDal();
        private MenuDal menuDal = new MenuDal();
        private UrunDal urunDal = new UrunDal();
        private MasaHareketleriEntity _entity;
        private RestoranContext context = new RestoranContext();
        public bool kaydet = false;

        // Finansal değişkenler
        private decimal toplamTutar = 0;
        private decimal indirimOrani = 0;
        private decimal indirimTutari = 0;
        private decimal indirimliToplam = 0;
        private decimal odenenTutar = 0;
        private decimal kalanTutar = 0;
        public frmMasaHareketKaydet(MasaHareketleriEntity entity)
        {
            InitializeComponent();
            _entity = entity;
            lookUpMasa.Properties.DataSource = masalarDal.GetAll(context);
            lookUpMasa.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "MasaId");
            lookUpMenu.Properties.DataSource = menuDal.GetAll(context);
            lookUpMenu.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "MenuId");
            lookUpUrun.Properties.DataSource = urunDal.GetAll(context);
            lookUpUrun.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "UrunId");
            txtSatisKodu.DataBindings.Add(propertyName: "Text", _entity, dataMember: "SatisKodu");
            calcMiktari.DataBindings.Add(propertyName: "Value", _entity, dataMember: "Miktari", formattingEnabled: true);
            calcBirimMiktari.DataBindings.Add(propertyName: "Value", _entity, dataMember: "BirimMiktarı", formattingEnabled: true);
            calcBirimFiyati.DataBindings.Add(propertyName: "Value", _entity, dataMember: "BirimFiyati", formattingEnabled: true);
            txtAciklama.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Aciklama");
            dateEditTarih.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "Tarih", formattingEnabled: true);
        }

        private void btnMasaHareketKaydet_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (lookUpMasa.EditValue == null)
            {
                XtraMessageBox.Show("Lütfen masa seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lookUpMasa.Focus();
                return;
            }

            if (calcMiktari.Value == null || Convert.ToDecimal(calcMiktari.Value) <= 0)
            {
                XtraMessageBox.Show("Lütfen geçerli bir miktar giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                calcMiktari.Focus();
                return;
            }

            if (masaHareketleriDal.AddOrUpdate(context, _entity))
            {
                masaHareketleriDal.Save(context);
                kaydet = true;
                XtraMessageBox.Show("Sipariş başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            if (kalanTutar > 0)
            {
                XtraMessageBox.Show($"Kalan tutar: {kalanTutar:C2}\nLütfen önce kalan tutarı ödeyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (XtraMessageBox.Show($"Toplam ödeme: {odenenTutar:C2}\nÖdemeyi onaylıyor musunuz?", "Ödeme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Ödeme işlemi burada yapılabilir (OdemeHareketleri tablosuna kayıt)
                XtraMessageBox.Show("Ödeme başarıyla alındı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnIndirimYap_Click(object sender, EventArgs e)
        {
            // İndirim zaten hesaplanıyor, sadece onay için
            if (indirimOrani > 0)
            {
                XtraMessageBox.Show($"İndirim Oranı: %{indirimOrani}\nİndirim Tutarı: {indirimTutari:C2}\nİndirimli Toplam: {indirimliToplam:C2}", 
                    "İndirim Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmMasaHareketKaydet_Load(object sender, EventArgs e)
        {
            // Rol bazlı yetkilendirme
            if (YetkiKontrolu.MevcutKullanici != null)
            {
                YetkiKontrolu.FormYetkileriniAyarla(this, YetkiKontrolu.MevcutKullaniciGorevi);
                RolBazliAyarlariYap();
            }

            // Finansal hesaplamaları başlat
            FinansalHesaplamalariYap();
            
            // Event handler'ları bağla
            calcMiktari.EditValueChanged += CalcMiktari_EditValueChanged;
            calcBirimFiyati.EditValueChanged += CalcBirimFiyati_EditValueChanged;
            calcIndirimOrani.EditValueChanged += CalcIndirimOrani_EditValueChanged;
            calcOdenenTutar.EditValueChanged += CalcOdenenTutar_EditValueChanged;
        }

        private void RolBazliAyarlariYap()
        {
            string gorev = YetkiKontrolu.MevcutKullaniciGorevi;

            // İndirim alanları - Sadece Yönetici ve Kasa değiştirebilir
            if (!YetkiKontrolu.RollerdenBiriVarMi(RollerSabitleri.Yonetici, RollerSabitleri.Kasa))
            {
                calcIndirimOrani.Enabled = false;
                calcIndirimOrani.Properties.ReadOnly = true;
            }

            // Ödeme al butonu - Sadece Yönetici ve Kasa
            YetkiKontrolu.ButonYetkisiAyarla(btnOdemeAl, new[] { RollerSabitleri.Yonetici, RollerSabitleri.Kasa }, false);

            // İndirim yap butonu - Sadece Yönetici ve Kasa
            YetkiKontrolu.ButonYetkisiAyarla(btnIndirimYap, new[] { RollerSabitleri.Yonetici, RollerSabitleri.Kasa }, false);

            // Müşteri için yönetimsel butonları gizle
            if (YetkiKontrolu.MusteriMi)
            {
                btnOdemeAl.Visible = false;
                btnIndirimYap.Visible = false;
                calcIndirimOrani.Visible = false;
                calcOdenenTutar.Visible = false;
                layoutControlItemIndirimOrani.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItemOdenenTutar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItemKalanTutar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        #region FİNANSAL HESAPLAMALAR

        private void FinansalHesaplamalariYap()
        {
            // Toplam tutar = Miktar * Birim Fiyat
            decimal miktar = calcMiktari.Value != null ? Convert.ToDecimal(calcMiktari.Value) : 0;
            decimal birimFiyat = calcBirimFiyati.Value != null ? Convert.ToDecimal(calcBirimFiyati.Value) : 0;
            toplamTutar = miktar * birimFiyat;

            // İndirim hesaplama
            indirimOrani = calcIndirimOrani.Value != null ? Convert.ToDecimal(calcIndirimOrani.Value) : 0;
            indirimTutari = toplamTutar * (indirimOrani / 100);
            indirimliToplam = toplamTutar - indirimTutari;

            // Ödeme hesaplama
            odenenTutar = calcOdenenTutar.Value != null ? Convert.ToDecimal(calcOdenenTutar.Value) : 0;
            kalanTutar = indirimliToplam - odenenTutar;

            // UI'ı güncelle
            txtToplamTutar.Text = toplamTutar.ToString("C2");
            txtIndirimTutari.Text = indirimTutari.ToString("C2");
            txtIndirimliToplam.Text = indirimliToplam.ToString("C2");
            txtKalanTutar.Text = kalanTutar.ToString("C2");

            // Kalan tutar renklendirme
            if (kalanTutar > 0)
            {
                txtKalanTutar.Properties.Appearance.ForeColor = Color.Red;
            }
            else if (kalanTutar == 0)
            {
                txtKalanTutar.Properties.Appearance.ForeColor = Color.Green;
            }
            else
            {
                txtKalanTutar.Properties.Appearance.ForeColor = Color.Orange;
            }
        }

        private void CalcMiktari_EditValueChanged(object sender, EventArgs e)
        {
            FinansalHesaplamalariYap();
        }

        private void CalcBirimFiyati_EditValueChanged(object sender, EventArgs e)
        {
            FinansalHesaplamalariYap();
        }

        private void CalcIndirimOrani_EditValueChanged(object sender, EventArgs e)
        {
            FinansalHesaplamalariYap();
        }

        private void CalcOdenenTutar_EditValueChanged(object sender, EventArgs e)
        {
            FinansalHesaplamalariYap();
        }

        #endregion

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
