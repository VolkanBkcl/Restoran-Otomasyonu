using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using RestoranOtomasyonu.Entities.DAL;
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
    public partial class frmUrunHareketleri : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private UrunHareketleriDal urunHareketleriDal = new UrunHareketleriDal();
            
        public frmUrunHareketleri()
        {
            InitializeComponent();
            Listele();
        }

        private void Listele()
        {
            gridControlUrunHareketleri.DataSource = urunHareketleriDal.GetAll(context);
            gridViewUrunHareketleri.BestFitColumns();
        }

        private void txtAra_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAra.Text))
            {
                gridViewUrunHareketleri.ActiveFilterString = "";
                return;
            }

            string filterString = $"[Aciklama] LIKE '%{txtAra.Text}%'";
            gridViewUrunHareketleri.ActiveFilterString = filterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.FileName = "UrunHareketleri_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControlUrunHareketleri.ExportToXlsx(saveFileDialog.FileName);
                MessageBox.Show("Dosya başarıyla dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridControlUrunHareketleri_Click(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            frmUrunHareketKaydet frm = new frmUrunHareketKaydet(entity:new UrunHareketleriEntity());
            frm.ShowDialog();
            if (frm.kaydet)
            {
                Listele(); 
            }
            Listele();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            int seciliid = Convert.ToInt32(gridViewUrunHareketleri.GetFocusedRowCellValue(colId));
            frmUrunHareketKaydet frm = new frmUrunHareketKaydet(entity:urunHareketleriDal.GetByFilter(context,u=>u.Id==seciliid));
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
            int seciliid = Convert.ToInt32(gridViewUrunHareketleri.GetFocusedRowCellValue(colId));
            frmUrunHareketKaydet frm = new frmUrunHareketKaydet(urunHareketleriDal.GetByFilter(context,u=>u.Id==seciliid));
            frm.ShowDialog();
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int seciliId = Convert.ToInt32(gridViewUrunHareketleri.GetFocusedRowCellValue(colId));
            if (MessageBox.Show("Seçili kayıt silinecek. Onaylıyor musunuz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                urunHareketleriDal.Delete(context, filter: u =>u.Id==seciliId);
                urunHareketleriDal.Save(context);
                Listele();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

