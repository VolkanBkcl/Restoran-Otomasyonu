using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Intefaces;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private bool _resimDegisti = false;
        public frmUrunKaydet(Urun entity)
        {
            InitializeComponent();
            this.Shown += frmUrunKaydet_Shown;
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
                if (_entity.Resim != null && !string.IsNullOrWhiteSpace(_entity.Resim))
                {
                    string resimYolu = _entity.Resim;
                    resimYolu = resimYolu.Replace('/', '\\');
                    
                    if (!Path.IsPathRooted(resimYolu))
                    {
                        resimYolu = Path.Combine(Application.StartupPath, resimYolu);
                    }
                    
                    if (File.Exists(resimYolu))
                    {
                        byte[] imageBytes = File.ReadAllBytes(resimYolu);
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            pictureEdit1.Image = Image.FromStream(ms);
                        }
                    }
                }
            }


        }

        private void frmUrunKaydet_Shown(object sender, EventArgs e)
        {
             _resimDegisti = false;
             pictureEdit1.EditValueChanged += (s, ev) => 
             {
                 _resimDegisti = true;
             };
        }

        private void btnUrunKaydet_Click(object sender, EventArgs e)
        {
            if (pictureEdit1.Image != null && (_resimDegisti || _entity.Id == 0))
            {
                string kaynakDosya = pictureEdit1.GetLoadedImageLocation();
                string dosyaAdi = "";

                if (!string.IsNullOrEmpty(kaynakDosya))
                {
                    dosyaAdi = Path.GetFileName(kaynakDosya);
                }
                else
                {
                    string safeUrunAdi = string.Join("_", txtUrunAdi.Text.Split(Path.GetInvalidFileNameChars()));
                    dosyaAdi = $"{safeUrunAdi}_{Guid.NewGuid().ToString().Substring(0, 8)}.png";
                }

                string winFormsImageKlasor = Path.Combine(Application.StartupPath, "Image");
                if (!Directory.Exists(winFormsImageKlasor)) Directory.CreateDirectory(winFormsImageKlasor);
                
                string winFormsHedefYol = Path.Combine(winFormsImageKlasor, dosyaAdi);


                bool dosyaKaydedildi = false;
                try
                {

                    if (!string.IsNullOrEmpty(kaynakDosya) && File.Exists(kaynakDosya))
                    {
                        File.Copy(kaynakDosya, winFormsHedefYol, true);
                    }
                    else
                    {
                        pictureEdit1.Image.Save(winFormsHedefYol);
                    }
                    dosyaKaydedildi = true;
                }
                catch (IOException)
                {

                    string uzanti = Path.GetExtension(dosyaAdi);
                    string simdi = DateTime.Now.ToString("yyyyMMddHHmmss");
                    dosyaAdi = $"{Path.GetFileNameWithoutExtension(dosyaAdi)}_{simdi}{uzanti}";
                    winFormsHedefYol = Path.Combine(winFormsImageKlasor, dosyaAdi);

                    try
                    {
                        if (!string.IsNullOrEmpty(kaynakDosya) && File.Exists(kaynakDosya))
                        {
                            File.Copy(kaynakDosya, winFormsHedefYol, true);
                        }
                        else
                        {
                            pictureEdit1.Image.Save(winFormsHedefYol);
                        }
                        dosyaKaydedildi = true;
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show($"Resim alternatif isimle de kaydedilemedi: {ex2.Message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Resim kaydedilirken hata: {ex.Message}");
                }

                if (dosyaKaydedildi)
                {
                    _entity.Resim = $"Image/{dosyaAdi}";
                    

                    try
                    {
                        string solutionKoku = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", ".."));
                        string webApiImageKlasor = Path.Combine(solutionKoku, "RestoranOtomasyonu.WebAPI", "wwwroot", "Image");
                        if (!Directory.Exists(webApiImageKlasor)) Directory.CreateDirectory(webApiImageKlasor);
                        
                        string webApiHedefYol = Path.Combine(webApiImageKlasor, dosyaAdi);
                        File.Copy(winFormsHedefYol, webApiHedefYol, true);
                    }
                    catch { /* Sessiz hata */ }
                }
            }


            
            bool basarili = false;
            Urun eskiVeri = null;
            int tur = 0;

            if (_entity.Id != 0)
            {
                var managedEntity = urunDal.GetByFilter(context, u => u.Id == _entity.Id);
                if (managedEntity != null)
                {
                    eskiVeri = new Urun
                    {
                        Id = managedEntity.Id,
                        MenuId = managedEntity.MenuId,
                        UrunKodu = managedEntity.UrunKodu,
                        UrunAdi = managedEntity.UrunAdi,
                        BirimFiyati1 = managedEntity.BirimFiyati1,
                        BirimFiyati2 = managedEntity.BirimFiyati2,
                        BirimFiyati3 = managedEntity.BirimFiyati3,
                        Aciklama = managedEntity.Aciklama,
                        Resim = managedEntity.Resim,
                        Tarih = managedEntity.Tarih
                    };
                    
                    managedEntity.MenuId = (int)lookUpMenu.EditValue;
                    managedEntity.UrunKodu = txtUrunKodu.Text;
                    managedEntity.UrunAdi = txtUrunAdi.Text;
                    managedEntity.BirimFiyati1 = Convert.ToDecimal(calcBirimFiyati1.Text);
                    managedEntity.BirimFiyati2 = Convert.ToDecimal(calcBirimFiyati2.Text);
                    managedEntity.BirimFiyati3 = Convert.ToDecimal(calcBirimFiyati3.Text);
                    managedEntity.Aciklama = txtAciklama.Text;
                    managedEntity.Tarih = dateEdit1.Text;
                    
                    if (!string.IsNullOrEmpty(_entity.Resim))
                    {
                        managedEntity.Resim = _entity.Resim;
                    }

                    basarili = urunDal.AddOrUpdate(context, managedEntity);
                    tur = 2;
                    tur = 2;
                    eskiVeri = managedEntity;
                }
            }
            else
            {
                basarili = urunDal.AddOrUpdate(context, _entity);
                tur = 0;
            }

            if (basarili)
            {
                UrunLogHelper.KayitEkle(context, eskiVeri, _entity, tur);
                urunDal.Save(context);
                kaydet = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ürün kaydedilemedi. Lütfen alanları kontrol ediniz.");
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