using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.Entities.Tools;
using MenuEntity = RestoranOtomasyonu.Entities.Models.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Menular
{
    public partial class frmMenuler : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        public frmMenuler()
        {
            InitializeComponent();
            // EF Core paketleri yerine, Entities projesindeki EF6 context'i kullanıyoruz.
            // Menüleri basitçe listeleyip grid'e bağlıyoruz.
            var menus = context.Menu.ToList();
            gridControl1.DataSource = menus;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Değişiklikleri kaydetmeden önce log kayıtlarını oluştur
            var changes = context.ChangeTracker.Entries<MenuEntity>()
                .Where(entry =>
                    entry.State.ToString() == "Added" ||
                    entry.State.ToString() == "Modified" ||
                    entry.State.ToString() == "Deleted")
                .ToList();

            foreach (var entry in changes)
            {
                MenuEntity eskiVeri = null;
                MenuEntity yeniVeri = null;
                int tur = 0; // 0=Ekleme, 1=Silme, 2=Güncelleme

                var stateName = entry.State.ToString();

                if (stateName == "Added")
                {
                    yeniVeri = entry.Entity;
                    tur = 0; // Ekleme
                }
                else if (stateName == "Deleted")
                {
                    eskiVeri = entry.Entity;
                    tur = 1; // Silme
                }
                else if (stateName == "Modified")
                {
                    // Güncelleme için eski değerleri almak için OriginalValues kullanıyoruz
                    var originalId = entry.Entity.Id;
                    if (originalId != 0)
                    {
                        // Eski değerleri manuel olarak oluşturuyoruz
                        eskiVeri = new MenuEntity
                        {
                            Id = originalId,
                            MenuAdi = entry.OriginalValues["MenuAdi"]?.ToString() ?? string.Empty,
                            Aciklama = entry.OriginalValues["Aciklama"]?.ToString() ?? string.Empty
                        };
                    }
                    yeniVeri = entry.Entity;
                    tur = 2; // Güncelleme
                }

                if (eskiVeri != null || yeniVeri != null)
                {
                    MenuLogHelper.KayitEkle(context, eskiVeri, yeniVeri, tur);
                }
            }

            context.SaveChanges();
            gridView1.RefreshData();
            MessageBox.Show("Değişiklikler kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtAra_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAra.Text))
            {
                gridView1.ActiveFilterString = "";
                return;
            }

            string filterString = $"[MenuAdi] LIKE '%{txtAra.Text}%' OR [Aciklama] LIKE '%{txtAra.Text}%'";
            gridView1.ActiveFilterString = filterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.FileName = "Menuler_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControl1.ExportToXlsx(saveFileDialog.FileName);
                MessageBox.Show("Dosya başarıyla dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili olan menü silinsin mi?","Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Silmeden önce seçili menüleri al
                var selectedRows = gridView1.GetSelectedRows();
                foreach (var rowHandle in selectedRows)
                {
                    var menu = gridView1.GetRow(rowHandle) as MenuEntity;
                    if (menu != null)
                    {
                        // Log kaydı ekle
                        MenuLogHelper.KayitEkle(context, menu, null, 1); // 1 = Silme
                    }
                }

                gridView1.DeleteSelectedRows();
                context.SaveChanges();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMenuler_Load(object sender, EventArgs e)
        {

        }
    }
} 