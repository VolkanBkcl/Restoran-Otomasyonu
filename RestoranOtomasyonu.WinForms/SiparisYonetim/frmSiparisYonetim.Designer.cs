namespace RestoranOtomasyonu.WinForms.SiparisYonetim
{
    partial class frmSiparisYonetim
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSiparisYonetim));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnDurumDegistir = new DevExpress.XtraEditors.SimpleButton();
            this.btnDetaylar = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlSiparisler = new DevExpress.XtraGrid.GridControl();
            this.gridViewSiparisler = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSiparisler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSiparisler)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(0, 12, 0, 12);
            this.labelControl1.Size = new System.Drawing.Size(1200, 60);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Sipariş Yönetimi";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 560);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(12);
            this.panelControl1.Size = new System.Drawing.Size(1200, 80);
            this.panelControl1.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnDurumDegistir);
            this.groupControl1.Controls.Add(this.btnDetaylar);
            this.groupControl1.Controls.Add(this.btnYenile);
            this.groupControl1.Controls.Add(this.btnKapat);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1176, 56);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "İşlemler";
            // 
            // btnDurumDegistir
            // 
            this.btnDurumDegistir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDurumDegistir.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDurumDegistir.Appearance.Options.UseFont = true;
            this.btnDurumDegistir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDurumDegistir.ImageOptions.Image")));
            this.btnDurumDegistir.Location = new System.Drawing.Point(850, 25);
            this.btnDurumDegistir.Name = "btnDurumDegistir";
            this.btnDurumDegistir.Size = new System.Drawing.Size(150, 30);
            this.btnDurumDegistir.TabIndex = 3;
            this.btnDurumDegistir.Text = "Durum Değiştir";
            this.btnDurumDegistir.Click += new System.EventHandler(this.btnDurumDegistir_Click);
            // 
            // btnDetaylar
            // 
            this.btnDetaylar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetaylar.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDetaylar.Appearance.Options.UseFont = true;
            this.btnDetaylar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDetaylar.ImageOptions.Image")));
            this.btnDetaylar.Location = new System.Drawing.Point(1010, 25);
            this.btnDetaylar.Name = "btnDetaylar";
            this.btnDetaylar.Size = new System.Drawing.Size(150, 30);
            this.btnDetaylar.TabIndex = 2;
            this.btnDetaylar.Text = "Ayrıntılar";
            this.btnDetaylar.Click += new System.EventHandler(this.btnDetaylar_Click);
            // 
            // btnYenile
            // 
            this.btnYenile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnYenile.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnYenile.Appearance.Options.UseFont = true;
            this.btnYenile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnYenile.ImageOptions.Image")));
            this.btnYenile.Location = new System.Drawing.Point(5, 25);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(100, 30);
            this.btnYenile.TabIndex = 1;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            // 
            // btnKapat
            // 
            this.btnKapat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKapat.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKapat.Appearance.Options.UseFont = true;
            this.btnKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKapat.ImageOptions.Image")));
            this.btnKapat.Location = new System.Drawing.Point(690, 25);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(150, 30);
            this.btnKapat.TabIndex = 0;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // gridControlSiparisler
            // 
            this.gridControlSiparisler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSiparisler.Location = new System.Drawing.Point(0, 60);
            this.gridControlSiparisler.MainView = this.gridViewSiparisler;
            this.gridControlSiparisler.Name = "gridControlSiparisler";
            this.gridControlSiparisler.Size = new System.Drawing.Size(1200, 500);
            this.gridControlSiparisler.TabIndex = 3;
            this.gridControlSiparisler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSiparisler});
            // 
            // gridViewSiparisler
            // 
            this.gridViewSiparisler.GridControl = this.gridControlSiparisler;
            this.gridViewSiparisler.Name = "gridViewSiparisler";
            this.gridViewSiparisler.OptionsView.ShowGroupPanel = false;
            // 
            // frmSiparisYonetim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 640);
            this.Controls.Add(this.gridControlSiparisler);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelControl1);
            this.Name = "frmSiparisYonetim";
            this.Text = "Sipariş Yönetimi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSiparisler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSiparisler)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraEditors.SimpleButton btnDetaylar;
        private DevExpress.XtraEditors.SimpleButton btnDurumDegistir;
        private DevExpress.XtraGrid.GridControl gridControlSiparisler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSiparisler;
    }
}
