namespace RestoranOtomasyonu.WinForms.SiparisYonetim
{
    partial class frmSiparisDetay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSiparisDetay));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTarih = new DevExpress.XtraEditors.LabelControl();
            this.lblDurum = new DevExpress.XtraEditors.LabelControl();
            this.lblKullaniciSayisi = new DevExpress.XtraEditors.LabelControl();
            this.lblSiparisSayisi = new DevExpress.XtraEditors.LabelControl();
            this.lblNetTutar = new DevExpress.XtraEditors.LabelControl();
            this.lblToplamTutar = new DevExpress.XtraEditors.LabelControl();
            this.lblMasaAdi = new DevExpress.XtraEditors.LabelControl();
            this.gridControlDetaylar = new DevExpress.XtraGrid.GridControl();
            this.gridViewDetaylar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDetaylar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetaylar)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblTarih);
            this.panelControl1.Controls.Add(this.lblDurum);
            this.panelControl1.Controls.Add(this.lblKullaniciSayisi);
            this.panelControl1.Controls.Add(this.lblSiparisSayisi);
            this.panelControl1.Controls.Add(this.lblNetTutar);
            this.panelControl1.Controls.Add(this.lblToplamTutar);
            this.panelControl1.Controls.Add(this.lblMasaAdi);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(15);
            this.panelControl1.Size = new System.Drawing.Size(900, 120);
            this.panelControl1.TabIndex = 0;
            // 
            // lblMasaAdi
            // 
            this.lblMasaAdi.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMasaAdi.Appearance.Options.UseFont = true;
            this.lblMasaAdi.Location = new System.Drawing.Point(15, 15);
            this.lblMasaAdi.Name = "lblMasaAdi";
            this.lblMasaAdi.Size = new System.Drawing.Size(60, 21);
            this.lblMasaAdi.TabIndex = 0;
            this.lblMasaAdi.Text = "Masa: -";
            // 
            // lblToplamTutar
            // 
            this.lblToplamTutar.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblToplamTutar.Appearance.Options.UseFont = true;
            this.lblToplamTutar.Location = new System.Drawing.Point(15, 45);
            this.lblToplamTutar.Name = "lblToplamTutar";
            this.lblToplamTutar.Size = new System.Drawing.Size(85, 17);
            this.lblToplamTutar.TabIndex = 1;
            this.lblToplamTutar.Text = "Toplam Tutar: -";
            // 
            // lblNetTutar
            // 
            this.lblNetTutar.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNetTutar.Appearance.Options.UseFont = true;
            this.lblNetTutar.Location = new System.Drawing.Point(15, 68);
            this.lblNetTutar.Name = "lblNetTutar";
            this.lblNetTutar.Size = new System.Drawing.Size(68, 17);
            this.lblNetTutar.TabIndex = 2;
            this.lblNetTutar.Text = "Net Tutar: -";
            // 
            // lblSiparisSayisi
            // 
            this.lblSiparisSayisi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSiparisSayisi.Appearance.Options.UseFont = true;
            this.lblSiparisSayisi.Location = new System.Drawing.Point(300, 15);
            this.lblSiparisSayisi.Name = "lblSiparisSayisi";
            this.lblSiparisSayisi.Size = new System.Drawing.Size(90, 17);
            this.lblSiparisSayisi.TabIndex = 3;
            this.lblSiparisSayisi.Text = "Sipariş Sayısı: -";
            // 
            // lblKullaniciSayisi
            // 
            this.lblKullaniciSayisi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKullaniciSayisi.Appearance.Options.UseFont = true;
            this.lblKullaniciSayisi.Location = new System.Drawing.Point(300, 45);
            this.lblKullaniciSayisi.Name = "lblKullaniciSayisi";
            this.lblKullaniciSayisi.Size = new System.Drawing.Size(100, 17);
            this.lblKullaniciSayisi.TabIndex = 4;
            this.lblKullaniciSayisi.Text = "Kullanıcı Sayısı: -";
            // 
            // lblDurum
            // 
            this.lblDurum.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDurum.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblDurum.Appearance.Options.UseFont = true;
            this.lblDurum.Appearance.Options.UseForeColor = true;
            this.lblDurum.Location = new System.Drawing.Point(300, 68);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(60, 17);
            this.lblDurum.TabIndex = 5;
            this.lblDurum.Text = "Durum: -";
            // 
            // lblTarih
            // 
            this.lblTarih.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTarih.Appearance.Options.UseFont = true;
            this.lblTarih.Location = new System.Drawing.Point(600, 15);
            this.lblTarih.Name = "lblTarih";
            this.lblTarih.Size = new System.Drawing.Size(50, 15);
            this.lblTarih.TabIndex = 6;
            this.lblTarih.Text = "Tarih: -";
            // 
            // gridControlDetaylar
            // 
            this.gridControlDetaylar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDetaylar.Location = new System.Drawing.Point(0, 120);
            this.gridControlDetaylar.MainView = this.gridViewDetaylar;
            this.gridControlDetaylar.Name = "gridControlDetaylar";
            this.gridControlDetaylar.Size = new System.Drawing.Size(900, 400);
            this.gridControlDetaylar.TabIndex = 1;
            this.gridControlDetaylar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetaylar});
            // 
            // gridViewDetaylar
            // 
            this.gridViewDetaylar.GridControl = this.gridControlDetaylar;
            this.gridViewDetaylar.Name = "gridViewDetaylar";
            this.gridViewDetaylar.OptionsView.ShowGroupPanel = false;
            // 
            // btnKapat
            // 
            this.btnKapat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKapat.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKapat.Appearance.Options.UseFont = true;
            this.btnKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKapat.ImageOptions.Image")));
            this.btnKapat.Location = new System.Drawing.Point(750, 530);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(140, 30);
            this.btnKapat.TabIndex = 2;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // frmSiparisDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 520);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.gridControlDetaylar);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmSiparisDetay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sipariş Detayları";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDetaylar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetaylar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblMasaAdi;
        private DevExpress.XtraEditors.LabelControl lblToplamTutar;
        private DevExpress.XtraEditors.LabelControl lblNetTutar;
        private DevExpress.XtraEditors.LabelControl lblSiparisSayisi;
        private DevExpress.XtraEditors.LabelControl lblKullaniciSayisi;
        private DevExpress.XtraEditors.LabelControl lblDurum;
        private DevExpress.XtraEditors.LabelControl lblTarih;
        private DevExpress.XtraGrid.GridControl gridControlDetaylar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDetaylar;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
    }
}
