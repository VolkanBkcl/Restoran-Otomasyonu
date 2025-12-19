using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Urunler
{
    public partial class frmUrunler : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private UrunDal urunDal = new UrunDal();
            
        public frmUrunler()
        {
            InitializeComponent();
            Listele();
        }

        private void Listele()
        {
            gridControl1.DataSource = urunDal.UrunListele(context);
            gridView1.BestFitColumns();
        }

        private void txtAra_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAra.Text))
            {
                gridView1.ActiveFilterString = "";
                return;
            }

            string filterString = $"[UrunAdi] LIKE '%{txtAra.Text}%' OR [UrunKodu] LIKE '%{txtAra.Text}%' OR [Aciklama] LIKE '%{txtAra.Text}%'";
            gridView1.ActiveFilterString = filterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.FileName = "Urunler_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControl1.ExportToXlsx(saveFileDialog.FileName);
                MessageBox.Show("Dosya başarıyla dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            frmUrunKaydet frm = new frmUrunKaydet(entity:new Urun());
            frm.ShowDialog();
            if (frm.kaydet)
            {
                Listele(); 
            }
            Listele();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            int seciliid = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colId));
            frmUrunKaydet frm = new frmUrunKaydet(entity:urunDal.GetByFilter(context,u=>u.Id==seciliid));
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
            int seciliid = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colId));
            frmUrunKaydet frm = new frmUrunKaydet(urunDal.GetByFilter(context,u=>u.Id==seciliid));
            frm.ShowDialog();
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int seciliId = Convert.ToInt32(gridView1.GetFocusedRowCellValue(colId));
            if (MessageBox.Show("Seçili kayıt silinecek. Onaylıyor musunuz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                // Silmeden önce eski veriyi al (log için)
                var eskiVeri = urunDal.GetByFilter(context, u => u.Id == seciliId);
                
                if (eskiVeri != null)
                {
                    // Log kaydı ekle
                    UrunLogHelper.KayitEkle(context, eskiVeri, null, 1); // 1 = Silme
                }

                urunDal.Delete(context, filter: u =>u.Id==seciliId);
                urunDal.Save(context);
                Listele();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}