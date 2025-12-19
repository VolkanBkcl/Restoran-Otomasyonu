using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Urunler
{
    public partial class frmUrunKaydet : DevExpress.XtraEditors.XtraForm
    {
        private MenuDal menudal = new MenuDal();
        private UrunDal urunDal = new UrunDal();
        private Urun _entity;
        private RestoranContext context = new RestoranContext();
        public bool kaydet = false;
        public frmUrunKaydet(Urun entity)
        {
            InitializeComponent();
            _entity = entity;
            lookUpMenu.Properties.DataSource = menudal.GetAll(context);
            lookUpMenu.DataBindings.Add(propertyName: "EditValue", _entity, dataMember: "MenuId");
            txtUrunKodu.DataBindings.Add(propertyName: "Text", _entity, dataMember: "UrunKodu");
            txtUrunAdi.DataBindings.Add(propertyName:"Text", _entity,dataMember: "UrunAdi");
            calcBirimFiyati1.DataBindings.Add(propertyName: "Text", _entity, dataMember: "BirimFiyati1",formattingEnabled:true);
            calcBirimFiyati2.DataBindings.Add(propertyName: "Text", _entity, dataMember: "BirimFiyati2",formattingEnabled: true);
            calcBirimFiyati3.DataBindings.Add(propertyName: "Text", _entity, dataMember: "BirimFiyati3",formattingEnabled: true);
            txtAciklama.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Aciklama");
            dateEdit1.DataBindings.Add(propertyName: "Text", _entity, dataMember: "Tarih",formattingEnabled:true);
            if (_entity.Id != 0)
            {
                if (_entity.Resim!= null)
                {
                    pictureEdit1.Image=Image.FromFile(_entity.Resim);
                }
            }

        }

        private void btnUrunKaydet_Click(object sender, EventArgs e)
        {
            if (pictureEdit1.GetLoadedImageLocation() !="") 
            {
                string hedefYol = $"{Application.StartupPath}\\Image\\{txtUrunAdi.Text}-{txtUrunKodu.Text}.png";
                File.Copy(sourceFileName: pictureEdit1.GetLoadedImageLocation(), destFileName: hedefYol);
                _entity.Resim = $"Image\\{txtUrunAdi.Text}-{txtUrunKodu.Text}.png"; 
            }

            // Güncelleme ise eski veriyi AsNoTracking ile çekelim (log için)
            Urun eskiVeri = null;
            int tur = 0; // 0=Ekleme, 2=Güncelleme

            if (_entity.Id != 0)
            {
                // Güncelleme - eski veriyi al
                eskiVeri = context.Set<Urun>()
                    .AsNoTracking()
                    .SingleOrDefault(u => u.Id == _entity.Id);
                tur = 2; // Güncelleme
            }
            else
            {
                tur = 0; // Ekleme
            }

            if (urunDal.AddOrUpdate(context, _entity))
            {
                // Log kaydı ekle
                UrunLogHelper.KayitEkle(context, eskiVeri, _entity, tur);

                urunDal.Save(context);
                kaydet = true;
                this.Close();
            }
        }

        private void frmUrunKaydet_Load(object sender, EventArgs e)
        {

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}