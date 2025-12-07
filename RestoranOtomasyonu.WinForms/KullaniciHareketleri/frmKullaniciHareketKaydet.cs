using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Models;
using KullaniciHareketleriEntity = RestoranOtomasyonu.Entities.Models.KullaniciHareketleri;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.KullaniciHareketleri
{
    public partial class frmKullaniciHareketKaydet : DevExpress.XtraEditors.XtraForm
    {
        private KullaniciHareketleriDal kullaniciHareketleriDal = new KullaniciHareketleriDal();
        private KullanicilarDal kullanicilarDal = new KullanicilarDal();
        private KullaniciHareketleriEntity _entity;
        private RestoranContext context = new RestoranContext();
        public bool kaydet = false;
        public frmKullaniciHareketKaydet(KullaniciHareketleriEntity entity)
        {
            InitializeComponent();
            _entity = entity;
            lookUpKullanici.Properties.DataSource = kullanicilarDal.GetAll(context);
            lookUpKullanici.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "KullaniciId");
            txtAciklama.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Aciklama");
            dateEditTarih.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "Tarih", formattingEnabled: true);
        }

        private void btnKullaniciHareketKaydet_Click(object sender, EventArgs e)
        {
            if (kullaniciHareketleriDal.AddOrUpdate(context, _entity))
            {
                kullaniciHareketleriDal.Save(context);
                kaydet = true;
                this.Close();
            }
        }

        private void frmKullaniciHareketKaydet_Load(object sender, EventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
