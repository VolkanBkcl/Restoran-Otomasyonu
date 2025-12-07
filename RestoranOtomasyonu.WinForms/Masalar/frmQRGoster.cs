using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.Masalar
{
    public partial class frmQRGoster : XtraForm
    {
        public frmQRGoster(Bitmap qrBitmap, string qrUrl, string masaAdi)
        {
            InitializeComponent();
            pictureEditQR.Image = qrBitmap;
            txtQRUrl.Text = qrUrl;
            this.Text = $"QR Kod - {masaAdi}";
        }

        private void btnKopyala_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtQRUrl.Text);
            XtraMessageBox.Show("URL panoya kopyalandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

