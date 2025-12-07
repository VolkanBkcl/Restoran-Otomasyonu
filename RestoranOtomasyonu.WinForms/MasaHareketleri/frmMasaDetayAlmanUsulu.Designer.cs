namespace RestoranOtomasyonu.WinForms.MasaHareketleri
{
    partial class frmMasaDetayAlmanUsulu
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraGrid.GridControl gridControlAlmanUsulu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAlmanUsulu;
        private DevExpress.XtraEditors.LabelControl lblGenelToplam;
        private DevExpress.XtraEditors.LabelControl lblGenelOdenen;
        private DevExpress.XtraEditors.LabelControl lblGenelKalan;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
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
            this.gridControlAlmanUsulu = new DevExpress.XtraGrid.GridControl();
            this.gridViewAlmanUsulu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblGenelToplam = new DevExpress.XtraEditors.LabelControl();
            this.lblGenelOdenen = new DevExpress.XtraEditors.LabelControl();
            this.lblGenelKalan = new DevExpress.XtraEditors.LabelControl();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAlmanUsulu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAlmanUsulu)).BeginInit();
            this.SuspendLayout();

            // gridControlAlmanUsulu
            this.gridControlAlmanUsulu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlAlmanUsulu.Location = new System.Drawing.Point(0, 0);
            this.gridControlAlmanUsulu.MainView = this.gridViewAlmanUsulu;
            this.gridControlAlmanUsulu.Name = "gridControlAlmanUsulu";
            this.gridControlAlmanUsulu.Size = new System.Drawing.Size(1000, 500);
            this.gridControlAlmanUsulu.TabIndex = 0;
            this.gridControlAlmanUsulu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAlmanUsulu});

            // gridViewAlmanUsulu
            this.gridViewAlmanUsulu.GridControl = this.gridControlAlmanUsulu;
            this.gridViewAlmanUsulu.Name = "gridViewAlmanUsulu";
            this.gridViewAlmanUsulu.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewAlmanUsulu.OptionsView.ShowGroupPanel = true;
            this.gridViewAlmanUsulu.OptionsView.ShowIndicator = true;

            // lblGenelToplam
            this.lblGenelToplam.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGenelToplam.Appearance.Options.UseFont = true;
            this.lblGenelToplam.Location = new System.Drawing.Point(20, 520);
            this.lblGenelToplam.Name = "lblGenelToplam";
            this.lblGenelToplam.Size = new System.Drawing.Size(150, 21);
            this.lblGenelToplam.TabIndex = 1;
            this.lblGenelToplam.Text = "Genel Toplam: 0.00 ₺";

            // lblGenelOdenen
            this.lblGenelOdenen.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGenelOdenen.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblGenelOdenen.Appearance.Options.UseFont = true;
            this.lblGenelOdenen.Appearance.Options.UseForeColor = true;
            this.lblGenelOdenen.Location = new System.Drawing.Point(200, 520);
            this.lblGenelOdenen.Name = "lblGenelOdenen";
            this.lblGenelOdenen.Size = new System.Drawing.Size(120, 21);
            this.lblGenelOdenen.TabIndex = 2;
            this.lblGenelOdenen.Text = "Ödenen: 0.00 ₺";

            // lblGenelKalan
            this.lblGenelKalan.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGenelKalan.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblGenelKalan.Appearance.Options.UseFont = true;
            this.lblGenelKalan.Appearance.Options.UseForeColor = true;
            this.lblGenelKalan.Location = new System.Drawing.Point(350, 520);
            this.lblGenelKalan.Name = "lblGenelKalan";
            this.lblGenelKalan.Size = new System.Drawing.Size(100, 21);
            this.lblGenelKalan.TabIndex = 3;
            this.lblGenelKalan.Text = "Kalan: 0.00 ₺";

            // btnYenile
            this.btnYenile.Location = new System.Drawing.Point(800, 515);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(90, 30);
            this.btnYenile.TabIndex = 4;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);

            // btnKapat
            this.btnKapat.Location = new System.Drawing.Point(900, 515);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(90, 30);
            this.btnKapat.TabIndex = 5;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);

            // frmMasaDetayAlmanUsulu
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 560);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.lblGenelKalan);
            this.Controls.Add(this.lblGenelOdenen);
            this.Controls.Add(this.lblGenelToplam);
            this.Controls.Add(this.gridControlAlmanUsulu);
            this.Name = "frmMasaDetayAlmanUsulu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alman Usulü Masa Detay";
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAlmanUsulu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAlmanUsulu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

