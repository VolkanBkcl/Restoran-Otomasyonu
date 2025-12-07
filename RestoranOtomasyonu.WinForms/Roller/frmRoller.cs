using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using RestoranOtomasyonu.Entities.DAL;
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
    public partial class frmRoller : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private RollerDal rollerDal = new RollerDal();
            
        public frmRoller()
        {
            InitializeComponent();
            Listele();
        }

        private void Listele()
        {
            gridControlRoller.DataSource = rollerDal.GetAll(context);
            gridViewRoller.BestFitColumns();
        }

        private void txtAra_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAra.Text))
            {
                gridViewRoller.ActiveFilterString = "";
                return;
            }

            string filterString = $"[FormName] LIKE '%{txtAra.Text}%' OR [ControlName] LIKE '%{txtAra.Text}%' OR [ControlCaption] LIKE '%{txtAra.Text}%'";
            gridViewRoller.ActiveFilterString = filterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.FileName = "Roller_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControlRoller.ExportToXlsx(saveFileDialog.FileName);
                MessageBox.Show("Dosya başarıyla dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridControlRoller_Click(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            frmRolKaydet frm = new frmRolKaydet(entity:new RollerEntity());
            frm.ShowDialog();
            if (frm.kaydet)
            {
                Listele(); 
            }
            Listele();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            int seciliid = Convert.ToInt32(gridViewRoller.GetFocusedRowCellValue(colId));
            frmRolKaydet frm = new frmRolKaydet(entity:rollerDal.GetByFilter(context,r=>r.Id==seciliid));
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
            int seciliid = Convert.ToInt32(gridViewRoller.GetFocusedRowCellValue(colId));
            frmRolKaydet frm = new frmRolKaydet(rollerDal.GetByFilter(context,r=>r.Id==seciliid));
            frm.ShowDialog();
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int seciliId = Convert.ToInt32(gridViewRoller.GetFocusedRowCellValue(colId));
            if (MessageBox.Show("Seçili kayıt silinecek. Onaylıyor musunuz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                rollerDal.Delete(context, filter: r =>r.Id==seciliId);
                rollerDal.Save(context);
                Listele();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

