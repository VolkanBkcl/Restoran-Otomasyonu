using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Models;
using MasalarEntity = RestoranOtomasyonu.Entities.Models.Masalar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;

namespace RestoranOtomasyonu.WinForms.Masalar
{
    public partial class frmMasalar : DevExpress.XtraEditors.XtraForm
    {
        private RestoranContext context = new RestoranContext();
        private MasalarDal masalarDal = new MasalarDal();
            
        public frmMasalar()
        {
            InitializeComponent();
            Listele();
        }

        private void Listele()
        {
            gridControlMasalar.DataSource = masalarDal.GetAll(context);
            gridViewMasalar.BestFitColumns();
        }

        private void txtAra_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAra.Text))
            {
                gridViewMasalar.ActiveFilterString = "";
                return;
            }

            string filterString = $"[MasaAdi] LIKE '%{txtAra.Text}%' OR [Aciklama] LIKE '%{txtAra.Text}%' OR [Durumu] LIKE '%{txtAra.Text}%'";
            gridViewMasalar.ActiveFilterString = filterString;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";
            saveFileDialog.FileName = "Masalar_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControlMasalar.ExportToXlsx(saveFileDialog.FileName);
                MessageBox.Show("Dosya başarıyla dışa aktarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridControlMasalar_Click(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            frmMasaKaydet frm = new frmMasaKaydet(entity:new MasalarEntity());
            frm.ShowDialog();
            if (frm.kaydet)
            {
                Listele(); 
            }
            Listele();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            int seciliid = Convert.ToInt32(gridViewMasalar.GetFocusedRowCellValue(colId));
            frmMasaKaydet frm = new frmMasaKaydet(entity:masalarDal.GetByFilter(context,m=>m.Id==seciliid));
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
            int seciliid = Convert.ToInt32(gridViewMasalar.GetFocusedRowCellValue(colId));
            frmMasaKaydet frm = new frmMasaKaydet(masalarDal.GetByFilter(context,m=>m.Id==seciliid));
            frm.ShowDialog();
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int seciliId = Convert.ToInt32(gridViewMasalar.GetFocusedRowCellValue(colId));
            if (MessageBox.Show("Seçili kayıt silinecek. Onaylıyor musunuz?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                masalarDal.Delete(context, filter: m =>m.Id==seciliId);
                masalarDal.Save(context);
                Listele();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQRKodOlustur_Click(object sender, EventArgs e)
        {
            try
            {
                int seciliId = Convert.ToInt32(gridViewMasalar.GetFocusedRowCellValue(colId));
                var masa = masalarDal.GetByFilter(context, m => m.Id == seciliId);

                if (masa == null)
                {
                    XtraMessageBox.Show("Lütfen bir masa seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // LAN IP adresini al
                string lanIp = GetLocalIPAddress();
                string qrUrl = $"http://{lanIp}/masa/{masa.Id}";

                // QR Kod oluştur
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrUrl, QRCodeGenerator.ECCLevel.Q);
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Bitmap qrBitmap = qrCode.GetGraphic(20);

                        // QR kod göster
                        using (frmQRGoster frm = new frmQRGoster(qrBitmap, qrUrl, masa.MasaAdi))
                        {
                            frm.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"QR kod oluşturulurken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetLocalIPAddress()
        {
            try
            {
                // Tüm network interface'lerini kontrol et
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Aktif ve loopback olmayan interface'leri bul
                    if (ni.OperationalStatus == OperationalStatus.Up &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            // IPv4 adreslerini kontrol et
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                                !ip.Address.ToString().StartsWith("169.254")) // APIPA adreslerini atla
                            {
                                return ip.Address.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"IP adresi alınırken hata: {ex.Message}");
            }

            // Varsayılan olarak localhost döndür
            return "localhost:5000";
        }
    }
}


