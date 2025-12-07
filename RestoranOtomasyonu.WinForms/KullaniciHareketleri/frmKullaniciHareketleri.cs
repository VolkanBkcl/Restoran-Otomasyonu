using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Models;
using KullaniciHareketleriEntity = RestoranOtomasyonu.Entities.Models.KullaniciHareketleri;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.KullaniciHareketleri
{
    public partial class frmKullaniciHareketleri : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private KullaniciHareketleriDal kullaniciHareketleriDal = new KullaniciHareketleriDal();
            
        public frmKullaniciHareketleri()
        {
            InitializeComponent();
            Listele();
        }

        private void Listele()
        {
            gridControlKullaniciHareketleri.DataSource = kullaniciHareketleriDal.GetAll(context);
            gridViewKullaniciHareketleri.BestFitColumns();
        }

        private void txtAra_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAra.Text))
            {
                gridViewKullaniciHareketleri.ActiveFilterString = "";
                return;
            }

            string filterString = $"[Aciklama] LIKE '%{txtAra.Text}%'";
            gridViewKullaniciHareketleri.ActiveFilterString = filterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.FileName = "KullaniciHareketleri_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControlKullaniciHareketleri.ExportToXlsx(saveFileDialog.FileName);
                MessageBox.Show("Dosya başarıyla dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridControlKullaniciHareketleri_Click(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            frmKullaniciHareketKaydet frm = new frmKullaniciHareketKaydet(entity:new KullaniciHareketleriEntity());
            frm.ShowDialog();
            if (frm.kaydet)
            {
                Listele(); 
            }
            Listele();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            int seciliid = Convert.ToInt32(gridViewKullaniciHareketleri.GetFocusedRowCellValue(colId));
            frmKullaniciHareketKaydet frm = new frmKullaniciHareketKaydet(entity:kullaniciHareketleriDal.GetByFilter(context,k=>k.Id==seciliid));
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
            int seciliid = Convert.ToInt32(gridViewKullaniciHareketleri.GetFocusedRowCellValue(colId));
            frmKullaniciHareketKaydet frm = new frmKullaniciHareketKaydet(kullaniciHareketleriDal.GetByFilter(context,k=>k.Id==seciliid));
            frm.ShowDialog();
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int seciliId = Convert.ToInt32(gridViewKullaniciHareketleri.GetFocusedRowCellValue(colId));
            if (MessageBox.Show("Seçili kayıt silinecek. Onaylıyor musunuz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                kullaniciHareketleriDal.Delete(context, filter: k =>k.Id==seciliId);
                kullaniciHareketleriDal.Save(context);
                Listele();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

