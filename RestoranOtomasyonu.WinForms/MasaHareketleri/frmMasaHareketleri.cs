using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Models;
using MasaHareketleriEntity = RestoranOtomasyonu.Entities.Models.MasaHareketleri;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.MasaHareketleri
{
    public partial class frmMasaHareketleri : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private MasaHareketleriDal masaHareketleriDal = new MasaHareketleriDal();
            
        public frmMasaHareketleri()
        {
            InitializeComponent();
            Listele();
        }

        private void Listele()
        {
            gridControlMasaHareketleri.DataSource = masaHareketleriDal.GetAll(context);
            gridViewMasaHareketleri.BestFitColumns();
        }

        private void txtAra_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAra.Text))
            {
                gridViewMasaHareketleri.ActiveFilterString = "";
                return;
            }

            string filterString = $"[SatisKodu] LIKE '%{txtAra.Text}%' OR [Aciklama] LIKE '%{txtAra.Text}%'";
            gridViewMasaHareketleri.ActiveFilterString = filterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.FileName = "MasaHareketleri_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControlMasaHareketleri.ExportToXlsx(saveFileDialog.FileName);
                MessageBox.Show("Dosya başarıyla dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridControlMasaHareketleri_Click(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            frmMasaHareketKaydet frm = new frmMasaHareketKaydet(entity:new MasaHareketleriEntity());
            frm.ShowDialog();
            if (frm.kaydet)
            {
                Listele(); 
            }
            Listele();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            int seciliid = Convert.ToInt32(gridViewMasaHareketleri.GetFocusedRowCellValue(colId));
            frmMasaHareketKaydet frm = new frmMasaHareketKaydet(entity:masaHareketleriDal.GetByFilter(context,m=>m.Id==seciliid));
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
            int seciliid = Convert.ToInt32(gridViewMasaHareketleri.GetFocusedRowCellValue(colId));
            frmMasaHareketKaydet frm = new frmMasaHareketKaydet(masaHareketleriDal.GetByFilter(context,m=>m.Id==seciliid));
            frm.ShowDialog();
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int seciliId = Convert.ToInt32(gridViewMasaHareketleri.GetFocusedRowCellValue(colId));
            if (MessageBox.Show("Seçili kayıt silinecek. Onaylıyor musunuz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                masaHareketleriDal.Delete(context, filter: m =>m.Id==seciliId);
                masaHareketleriDal.Save(context);
                Listele();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

