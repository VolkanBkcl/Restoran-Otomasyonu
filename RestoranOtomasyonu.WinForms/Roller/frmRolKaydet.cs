using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Models;
using RollerEntity = RestoranOtomasyonu.Entities.Models.Roller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Roller
{
    public partial class frmRolKaydet : DevExpress.XtraEditors.XtraForm
    {
        private RollerDal rollerDal = new RollerDal();
        private KullanicilarDal kullanicilarDal = new KullanicilarDal();
        private RollerEntity _entity;
        private RestoranContext context = new RestoranContext();
        public bool kaydet = false;
        public frmRolKaydet(RollerEntity entity)
        {
            InitializeComponent();
            _entity = entity;
            lookUpKullanici.Properties.DataSource = kullanicilarDal.GetAll(context);
            lookUpKullanici.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "KullaniciId");
            txtFormName.DataBindings.Add(propertyName: "Text", _entity, dataMember: "FormName");
            txtControlName.DataBindings.Add(propertyName: "Text", _entity, dataMember: "ControlName");
            txtControlCaption.DataBindings.Add(propertyName: "Text", _entity, dataMember: "ControlCaption");
        }

        private void btnRolKaydet_Click(object sender, EventArgs e)
        {
            if (rollerDal.AddOrUpdate(context, _entity))
            {
                rollerDal.Save(context);
                kaydet = true;
                this.Close();
            }
        }

        private void frmRolKaydet_Load(object sender, EventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
