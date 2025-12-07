using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using RestoranOtomasyonu.Entities.DAL;
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
    public partial class frmMenuHareketleri : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private MenuHareketleriDal menuHareketleriDal = new MenuHareketleriDal();
            
        public frmMenuHareketleri()
        {
            InitializeComponent();
            Listele();
        }

        private void Listele()
        {
            gridControlMenuHareketleri.DataSource = menuHareketleriDal.GetAll(context);
            gridViewMenuHareketleri.BestFitColumns();
        }

        private void txtAra_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAra.Text))
            {
                gridViewMenuHareketleri.ActiveFilterString = "";
                return;
            }

            string filterString = $"[Aciklama] LIKE '%{txtAra.Text}%'";
            gridViewMenuHareketleri.ActiveFilterString = filterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.FileName = "MenuHareketleri_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControlMenuHareketleri.ExportToXlsx(saveFileDialog.FileName);
                MessageBox.Show("Dosya başarıyla dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridControlMenuHareketleri_Click(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            frmMenuHareketKaydet frm = new frmMenuHareketKaydet(entity:new MenuHareketleriEntity());
            frm.ShowDialog();
            if (frm.kaydet)
            {
                Listele(); 
            }
            Listele();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            int seciliid = Convert.ToInt32(gridViewMenuHareketleri.GetFocusedRowCellValue(colId));
            frmMenuHareketKaydet frm = new frmMenuHareketKaydet(entity:menuHareketleriDal.GetByFilter(context,m=>m.Id==seciliid));
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
            int seciliid = Convert.ToInt32(gridViewMenuHareketleri.GetFocusedRowCellValue(colId));
            frmMenuHareketKaydet frm = new frmMenuHareketKaydet(menuHareketleriDal.GetByFilter(context,m=>m.Id==seciliid));
            frm.ShowDialog();
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int seciliId = Convert.ToInt32(gridViewMenuHareketleri.GetFocusedRowCellValue(colId));
            if (MessageBox.Show("Seçili kayıt silinecek. Onaylıyor musunuz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                menuHareketleriDal.Delete(context, filter: m =>m.Id==seciliId);
                menuHareketleriDal.Save(context);
                Listele();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

