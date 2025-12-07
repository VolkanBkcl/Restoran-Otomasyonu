namespace RestoranOtomasyonu.WinForms.Masalar
{
    partial class frmQRGoster
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.PictureEdit pictureEditQR;
        private DevExpress.XtraEditors.TextEdit txtQRUrl;
        private DevExpress.XtraEditors.SimpleButton btnKopyala;
        private DevExpress.XtraEditors.SimpleButton btnKapat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureEditQR = new DevExpress.XtraEditors.PictureEdit();
            this.txtQRUrl = new DevExpress.XtraEditors.TextEdit();
            this.btnKopyala = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditQR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQRUrl.Properties)).BeginInit();
            this.SuspendLayout();

            // pictureEditQR
            this.pictureEditQR.Location = new System.Drawing.Point(20, 20);
            this.pictureEditQR.Name = "pictureEditQR";
            this.pictureEditQR.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEditQR.Size = new System.Drawing.Size(300, 300);
            this.pictureEditQR.TabIndex = 0;

            // txtQRUrl
            this.txtQRUrl.Location = new System.Drawing.Point(20, 340);
            this.txtQRUrl.Name = "txtQRUrl";
            this.txtQRUrl.Properties.ReadOnly = true;
            this.txtQRUrl.Size = new System.Drawing.Size(300, 22);
            this.txtQRUrl.TabIndex = 1;

            // btnKopyala
            this.btnKopyala.Location = new System.Drawing.Point(20, 380);
            this.btnKopyala.Name = "btnKopyala";
            this.btnKopyala.Size = new System.Drawing.Size(140, 40);
            this.btnKopyala.TabIndex = 2;
            this.btnKopyala.Text = "URL'yi Kopyala";
            this.btnKopyala.Click += new System.EventHandler(this.btnKopyala_Click);

            // btnKapat
            this.btnKapat.Location = new System.Drawing.Point(180, 380);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(140, 40);
            this.btnKapat.TabIndex = 3;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);

            // frmQRGoster
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 440);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnKopyala);
            this.Controls.Add(this.txtQRUrl);
            this.Controls.Add(this.pictureEditQR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQRGoster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QR Kod";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditQR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQRUrl.Properties)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

