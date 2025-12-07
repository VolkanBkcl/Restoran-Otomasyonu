using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Models;
using MasalarEntity = RestoranOtomasyonu.Entities.Models.Masalar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Masalar
{
    public partial class frmMasaKaydet : DevExpress.XtraEditors.XtraForm
    {
        private MasalarDal masalarDal = new MasalarDal();
        private MasalarEntity _entity;
        private RestoranContext context = new RestoranContext();
        public bool kaydet = false;
        public frmMasaKaydet(MasalarEntity entity)
        {
            InitializeComponent();
            _entity = entity;
            txtMasaAdi.DataBindings.Add(propertyName: "Text", _entity, dataMember: "MasaAdi");
            txtAciklama.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Aciklama");
            checkEditDurumu.DataBindings.Add(propertyName: "Checked", _entity, dataMember: "Durumu");
            checkEditRezervMi.DataBindings.Add(propertyName: "Checked", _entity, dataMember: "RezervMi");
            dateEditEklenmeTarihi.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "EklenmeTarihi", formattingEnabled: true);
            dateEditSonIslemTarihi.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "SonİslemTarihi", formattingEnabled: true);
        }

        private void btnMasaKaydet_Click(object sender, EventArgs e)
        {
            if (masalarDal.AddOrUpdate(context, _entity))
            {
                masalarDal.Save(context);
                kaydet = true;
                this.Close();
            }
        }

        private void frmMasaKaydet_Load(object sender, EventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

