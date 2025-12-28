namespace RestoranOtomasyonu.WinForms.SiparisYonetim
{
    partial class frmSiparisDurumSec
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSiparisDurumSec));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboDurum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnTamam = new DevExpress.XtraEditors.SimpleButton();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.comboDurum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(20, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(95, 17);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Yeni Durum Seç:";
            // 
            // comboDurum
            // 
            this.comboDurum.Location = new System.Drawing.Point(120, 22);
            this.comboDurum.Name = "comboDurum";
            this.comboDurum.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboDurum.Properties.Appearance.Options.UseFont = true;
            this.comboDurum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboDurum.Size = new System.Drawing.Size(250, 22);
            this.comboDurum.TabIndex = 1;
            // 
            // btnTamam
            // 
            this.btnTamam.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTamam.Appearance.Options.UseFont = true;
            this.btnTamam.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTamam.ImageOptions.Image")));
            this.btnTamam.Location = new System.Drawing.Point(200, 60);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(100, 30);
            this.btnTamam.TabIndex = 2;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);
            // 
            // btnIptal
            // 
            this.btnIptal.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnIptal.Appearance.Options.UseFont = true;
            this.btnIptal.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnIptal.ImageOptions.Image")));
            this.btnIptal.Location = new System.Drawing.Point(310, 60);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(100, 30);
            this.btnIptal.TabIndex = 3;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // frmSiparisDurumSec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 110);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnTamam);
            this.Controls.Add(this.comboDurum);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSiparisDurumSec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sipariş Durumu Seç";
            ((System.ComponentModel.ISupportInitialize)(this.comboDurum.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboDurum;
        private DevExpress.XtraEditors.SimpleButton btnTamam;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
    }
}
