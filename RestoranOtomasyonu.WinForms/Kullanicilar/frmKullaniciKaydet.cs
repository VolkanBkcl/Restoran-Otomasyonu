using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Tools;
using RestoranOtomasyonu.WinForms.Core;
using RollerSabitleri = RestoranOtomasyonu.WinForms.Core.Roller;
using KullanicilarEntity = RestoranOtomasyonu.Entities.Models.Kullanicilar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Kullanicilar
{
    public partial class frmKullaniciKaydet : DevExpress.XtraEditors.XtraForm
    {
        private KullanicilarDal kullanicilarDal = new KullanicilarDal();
        private KullanicilarEntity _entity;
        private RestoranContext context = new RestoranContext();
        public bool kaydet = false;
        public frmKullaniciKaydet(KullanicilarEntity entity)
        {
            InitializeComponent();
            _entity = entity;
            txtAdSoyad.DataBindings.Add(propertyName: "Text", _entity, dataMember: "AdSoyad");
            txtTelefon.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Telefon");
            txtAdres.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Adres");
            txtEmail.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Email");
            lookUpGorevi.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "Gorevi");
            txtKullaniciAdi.DataBindings.Add(propertyName: "Text", _entity, dataMember: "KullaniciAdi");
            txtParola.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Parola");
            txtHatirlatmaSorusu.DataBindings.Add(propertyName: "Text", _entity, dataMember: "HatirlatmaSorusu");
            txtCevap.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Cevap");
            txtAciklama.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Aciklama");
            checkEditAktifMi.DataBindings.Add(propertyName: "Checked", _entity, dataMember: "AktifMi");
            dateEditKayitTarihi.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "KayitTarihi", formattingEnabled: true);
        }

        private void btnKullaniciKaydet_Click(object sender, EventArgs e)
        {
            // Ekleme mi, güncelleme mi kontrol et
            KullanicilarEntity eskiVeri = null;
            int islemTuru; // 0 = Ekleme, 2 = Güncelleme

            if (_entity.Id != 0)
            {
                // Güncelleme: eski veriyi AsNoTracking ile çek
                eskiVeri = context.Set<KullanicilarEntity>()
                                  .AsNoTracking()
                                  .SingleOrDefault(k => k.Id == _entity.Id);
                islemTuru = 2; // Güncelleme
            }
            else
            {
                // Yeni kayıt: kayıt tarihi boş ise doldur
                if (_entity.KayitTarihi == default(DateTime))
                {
                    _entity.KayitTarihi = DateTime.Now;
                }

                islemTuru = 0; // Ekleme
            }

            if (kullanicilarDal.AddOrUpdate(context, _entity))
            {
                // Kullanıcı hareketleri tablosuna log kaydı ekle
                KullaniciLogHelper.KayitEkle(context, eskiVeri, _entity, islemTuru);

                // Tüm değişiklikleri tek seferde veritabanına yaz
                kullanicilarDal.Save(context);
                kaydet = true;
                this.Close();
            }
        }

        private void frmKullaniciKaydet_Load(object sender, EventArgs e)
        {
            // Roller ComboBox'ını doldur
            var rollerListesi = RollerSabitleri.TumRoller.Select(r => new { Rol = r }).ToList();
            lookUpGorevi.Properties.DataSource = rollerListesi;
            lookUpGorevi.Properties.DisplayMember = "Rol";
            lookUpGorevi.Properties.ValueMember = "Rol";

            // Rol bazlı yetkilendirme - Sadece Yönetici görev değiştirebilir
            if (YetkiKontrolu.MevcutKullanici != null && !YetkiKontrolu.YoneticiMi)
            {
                lookUpGorevi.Enabled = false;
                lookUpGorevi.Properties.ReadOnly = true;
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


