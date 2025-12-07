using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Models;
using MenuHareketleriEntity = RestoranOtomasyonu.Entities.Models.MenuHareketleri;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.MenuHareketleri
{
    public partial class frmMenuHareketKaydet : DevExpress.XtraEditors.XtraForm
    {
        private MenuHareketleriDal menuHareketleriDal = new MenuHareketleriDal();
        private MenuDal menuDal = new MenuDal();
        private MenuHareketleriEntity _entity;
        private RestoranContext context = new RestoranContext();
        public bool kaydet = false;
        public frmMenuHareketKaydet(MenuHareketleriEntity entity)
        {
            InitializeComponent();
            _entity = entity;
            lookUpMenu.Properties.DataSource = menuDal.GetAll(context);
            lookUpMenu.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "MenuId");
            txtSatisKodu.DataBindings.Add(propertyName: "Text", _entity, dataMember: "SatisKodu");
            calcMiktari.DataBindings.Add(propertyName: "Value", _entity, dataMember: "Miktari", formattingEnabled: true);
            calcBirimMiktari.DataBindings.Add(propertyName: "Value", _entity, dataMember: "BirimMiktarı", formattingEnabled: true);
            calcBirimFiyati.DataBindings.Add(propertyName: "Value", _entity, dataMember: "BirimFiyati", formattingEnabled: true);
            txtAciklama.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Aciklama");
            dateEditTarih.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "Tarih", formattingEnabled: true);
        }

        private void btnMenuHareketKaydet_Click(object sender, EventArgs e)
        {
            if (menuHareketleriDal.AddOrUpdate(context, _entity))
            {
                menuHareketleriDal.Save(context);
                kaydet = true;
                this.Close();
            }
        }

        private void frmMenuHareketKaydet_Load(object sender, EventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

