using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Models;
using UrunHareketleriEntity = RestoranOtomasyonu.Entities.Models.UrunHareketleri;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.UrunHareketleri
{
    public partial class frmUrunHareketKaydet : DevExpress.XtraEditors.XtraForm
    {
        private UrunHareketleriDal urunHareketleriDal = new UrunHareketleriDal();
        private UrunDal urunDal = new UrunDal();
        private UrunHareketleriEntity _entity;
        private RestoranContext context = new RestoranContext();
        public bool kaydet = false;
        public frmUrunHareketKaydet(UrunHareketleriEntity entity)
        {
            InitializeComponent();
            _entity = entity;
            lookUpUrun.Properties.DataSource = urunDal.GetAll(context);
            lookUpUrun.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "UrunId");
            txtSatisKodu.DataBindings.Add(propertyName: "Text", _entity, dataMember: "SatisKodu");
            calcMiktari.DataBindings.Add(propertyName: "Value", _entity, dataMember: "Miktari", formattingEnabled: true);
            calcBirimMiktari.DataBindings.Add(propertyName: "Value", _entity, dataMember: "BirimMiktarı", formattingEnabled: true);
            calcBirimFiyati.DataBindings.Add(propertyName: "Value", _entity, dataMember: "BirimFiyati", formattingEnabled: true);
            txtAciklama.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Aciklama");
            dateEditTarih.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "Tarih", formattingEnabled: true);
        }

        private void btnUrunHareketKaydet_Click(object sender, EventArgs e)
        {
            if (urunHareketleriDal.AddOrUpdate(context, _entity))
            {
                urunHareketleriDal.Save(context);
                kaydet = true;
                this.Close();
            }
        }

        private void frmUrunHareketKaydet_Load(object sender, EventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

