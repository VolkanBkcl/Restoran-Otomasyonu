using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Tools;
using RestoranOtomasyonu.WinForms.Core;
using KullanicilarEntity = RestoranOtomasyonu.Entities.Models.Kullanicilar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Kullanicilar
{
    public partial class frmKullanicilar : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private KullanicilarDal kullanicilarDal = new KullanicilarDal();
            
        public frmKullanicilar()
        {
            InitializeComponent();
            
            // DEBUG: Yetki kontrolü öncesi bilgileri logla
            System.Diagnostics.Debug.WriteLine("=== frmKullanicilar AÇILIŞ ===");
            System.Diagnostics.Debug.WriteLine($"MevcutKullanici null mu?: {YetkiKontrolu.MevcutKullanici == null}");
            if (YetkiKontrolu.MevcutKullanici != null)
            {
                System.Diagnostics.Debug.WriteLine($"Kullanıcı Görevi: '{YetkiKontrolu.MevcutKullanici.Gorevi}'");
                System.Diagnostics.Debug.WriteLine($"MevcutKullaniciGorevi: '{YetkiKontrolu.MevcutKullaniciGorevi}'");
                System.Diagnostics.Debug.WriteLine($"YoneticiMi: {YetkiKontrolu.YoneticiMi}");
            }
            System.Diagnostics.Debug.WriteLine("=============================");
            
            // Sadece Yönetici erişebilir
            if (YetkiKontrolu.MevcutKullanici == null || !YetkiKontrolu.YoneticiMi)
            {
                XtraMessageBox.Show("Bu işlem için Yönetici yetkisi gereklidir.", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            
            Listele();
        }

        private void Listele()
        {
            gridControlKullanicilar.DataSource = kullanicilarDal.GetAll(context);
            gridViewKullanicilar.BestFitColumns();
        }

        private void txtAra_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAra.Text))
            {
                gridViewKullanicilar.ActiveFilterString = "";
                return;
            }

            string filterString = $"[AdSoyad] LIKE '%{txtAra.Text}%' OR [Email] LIKE '%{txtAra.Text}%' OR [Telefon] LIKE '%{txtAra.Text}%' OR [KullaniciAdi] LIKE '%{txtAra.Text}%'";
            gridViewKullanicilar.ActiveFilterString = filterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.FileName = "Kullanicilar_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControlKullanicilar.ExportToXlsx(saveFileDialog.FileName);
                MessageBox.Show("Dosya başarıyla dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridControlKullanicilar_Click(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            frmKullaniciKaydet frm = new frmKullaniciKaydet(entity:new KullanicilarEntity());
            frm.ShowDialog();
            if (frm.kaydet)
            {
                Listele(); 
            }
            Listele();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            int seciliid = Convert.ToInt32(gridViewKullanicilar.GetFocusedRowCellValue(colId));
            frmKullaniciKaydet frm = new frmKullaniciKaydet(entity:kullanicilarDal.GetByFilter(context,k=>k.Id==seciliid));
            frm.ShowDialog();
            if (frm.kaydet)
            {
                Listele();
            }
            Listele();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            int seciliid = Convert.ToInt32(gridViewKullanicilar.GetFocusedRowCellValue(colId));
            frmKullaniciKaydet frm = new frmKullaniciKaydet(kullanicilarDal.GetByFilter(context,k=>k.Id==seciliid));
            frm.ShowDialog();
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int seciliId = Convert.ToInt32(gridViewKullanicilar.GetFocusedRowCellValue(colId));
            if (MessageBox.Show("Seçili kayıt silinecek. Onaylıyor musunuz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                // Silmeden önce eski veriyi al (log için)
                var eskiVeri = kullanicilarDal.GetByFilter(context, k => k.Id == seciliId);

                if (eskiVeri != null)
                {
                    // 1 = Silme
                    KullaniciLogHelper.KayitEkle(context, eskiVeri, null, 1);
                }

                kullanicilarDal.Delete(context, k => k.Id == seciliId);
                kullanicilarDal.Save(context);
                Listele();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


