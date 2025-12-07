namespace RestoranOtomasyonu.WinForms.Masalar
{
    partial class frmMasaKaydet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMasaKaydet));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.btnMasaKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dateEditSonIslemTarihi = new DevExpress.XtraEditors.DateEdit();
            this.dateEditEklenmeTarihi = new DevExpress.XtraEditors.DateEdit();
            this.checkEditRezervMi = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditDurumu = new DevExpress.XtraEditors.CheckEdit();
            this.txtAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.txtMasaAdi = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupTemel = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemMasaAdi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAciklama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupDurum = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemDurumu = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemRezervMi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupTarih = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemEklenmeTarihi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemSonIslemTarihi = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSonIslemTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSonIslemTarihi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEklenmeTarihi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEklenmeTarihi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditRezervMi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDurumu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMasaAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTemel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMasaAdi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAciklama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupDurum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDurumu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRezervMi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTarih)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEklenmeTarihi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSonIslemTarihi)).BeginInit();
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
            this.labelControl1.Size = new System.Drawing.Size(700, 60);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Masa Kayıt";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnKapat);
            this.groupControl1.Controls.Add(this.btnMasaKaydet);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 450);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Padding = new System.Windows.Forms.Padding(12);
            this.groupControl1.Size = new System.Drawing.Size(700, 70);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "İşlemler";
            // 
            // btnKapat
            // 
            this.btnKapat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnKapat.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKapat.Appearance.Options.UseFont = true;
            this.btnKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKapat.ImageOptions.Image")));
            this.btnKapat.Location = new System.Drawing.Point(590, 24);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(98, 40);
            this.btnKapat.TabIndex = 1;
            this.btnKapat.Text = "İptal";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // btnMasaKaydet
            // 
            this.btnMasaKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMasaKaydet.Appearance.Options.UseFont = true;
            this.btnMasaKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMasaKaydet.ImageOptions.Image")));
            this.btnMasaKaydet.Location = new System.Drawing.Point(24, 24);
            this.btnMasaKaydet.Name = "btnMasaKaydet";
            this.btnMasaKaydet.Size = new System.Drawing.Size(120, 40);
            this.btnMasaKaydet.TabIndex = 0;
            this.btnMasaKaydet.Text = "Kaydet";
            this.btnMasaKaydet.Click += new System.EventHandler(this.btnMasaKaydet_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dateEditSonIslemTarihi);
            this.layoutControl1.Controls.Add(this.dateEditEklenmeTarihi);
            this.layoutControl1.Controls.Add(this.checkEditRezervMi);
            this.layoutControl1.Controls.Add(this.checkEditDurumu);
            this.layoutControl1.Controls.Add(this.txtAciklama);
            this.layoutControl1.Controls.Add(this.txtMasaAdi);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 60);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1024, 0, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(700, 390);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dateEditSonIslemTarihi
            // 
            this.dateEditSonIslemTarihi.EditValue = null;
            this.dateEditSonIslemTarihi.Location = new System.Drawing.Point(140, 304);
            this.dateEditSonIslemTarihi.Name = "dateEditSonIslemTarihi";
            this.dateEditSonIslemTarihi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateEditSonIslemTarihi.Properties.Appearance.Options.UseFont = true;
            this.dateEditSonIslemTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditSonIslemTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditSonIslemTarihi.Size = new System.Drawing.Size(536, 22);
            this.dateEditSonIslemTarihi.StyleController = this.layoutControl1;
            this.dateEditSonIslemTarihi.TabIndex = 9;
            // 
            // dateEditEklenmeTarihi
            // 
            this.dateEditEklenmeTarihi.EditValue = null;
            this.dateEditEklenmeTarihi.Location = new System.Drawing.Point(140, 278);
            this.dateEditEklenmeTarihi.Name = "dateEditEklenmeTarihi";
            this.dateEditEklenmeTarihi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateEditEklenmeTarihi.Properties.Appearance.Options.UseFont = true;
            this.dateEditEklenmeTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEklenmeTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditEklenmeTarihi.Size = new System.Drawing.Size(536, 22);
            this.dateEditEklenmeTarihi.StyleController = this.layoutControl1;
            this.dateEditEklenmeTarihi.TabIndex = 8;
            // 
            // checkEditRezervMi
            // 
            this.checkEditRezervMi.Location = new System.Drawing.Point(140, 252);
            this.checkEditRezervMi.Name = "checkEditRezervMi";
            this.checkEditRezervMi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkEditRezervMi.Properties.Appearance.Options.UseFont = true;
            this.checkEditRezervMi.Properties.Caption = "Rezervasyonlu";
            this.checkEditRezervMi.Size = new System.Drawing.Size(536, 20);
            this.checkEditRezervMi.StyleController = this.layoutControl1;
            this.checkEditRezervMi.TabIndex = 7;
            // 
            // checkEditDurumu
            // 
            this.checkEditDurumu.Location = new System.Drawing.Point(140, 226);
            this.checkEditDurumu.Name = "checkEditDurumu";
            this.checkEditDurumu.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkEditDurumu.Properties.Appearance.Options.UseFont = true;
            this.checkEditDurumu.Properties.Caption = "Aktif";
            this.checkEditDurumu.Size = new System.Drawing.Size(536, 20);
            this.checkEditDurumu.StyleController = this.layoutControl1;
            this.checkEditDurumu.TabIndex = 6;
            // 
            // txtAciklama
            // 
            this.txtAciklama.Location = new System.Drawing.Point(140, 54);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAciklama.Properties.Appearance.Options.UseFont = true;
            this.txtAciklama.Size = new System.Drawing.Size(536, 168);
            this.txtAciklama.StyleController = this.layoutControl1;
            this.txtAciklama.TabIndex = 5;
            // 
            // txtMasaAdi
            // 
            this.txtMasaAdi.Location = new System.Drawing.Point(140, 28);
            this.txtMasaAdi.Name = "txtMasaAdi";
            this.txtMasaAdi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMasaAdi.Properties.Appearance.Options.UseFont = true;
            this.txtMasaAdi.Size = new System.Drawing.Size(536, 22);
            this.txtMasaAdi.StyleController = this.layoutControl1;
            this.txtMasaAdi.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupTemel,
            this.layoutControlGroupDurum,
            this.layoutControlGroupTarih});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(12, 12, 12, 12);
            this.layoutControlGroup1.Size = new System.Drawing.Size(700, 390);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupTemel
            // 
            this.layoutControlGroupTemel.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemMasaAdi,
            this.layoutControlItemAciklama});
            this.layoutControlGroupTemel.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupTemel.Name = "layoutControlGroupTemel";
            this.layoutControlGroupTemel.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 12);
            this.layoutControlGroupTemel.Size = new System.Drawing.Size(676, 198);
            this.layoutControlGroupTemel.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 8, 0);
            this.layoutControlGroupTemel.Text = "Temel Bilgiler";
            // 
            // layoutControlItemMasaAdi
            // 
            this.layoutControlItemMasaAdi.Control = this.txtMasaAdi;
            this.layoutControlItemMasaAdi.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemMasaAdi.Name = "layoutControlItemMasaAdi";
            this.layoutControlItemMasaAdi.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemMasaAdi.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemMasaAdi.Text = "Masa Adı:";
            this.layoutControlItemMasaAdi.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemAciklama
            // 
            this.layoutControlItemAciklama.Control = this.txtAciklama;
            this.layoutControlItemAciklama.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemAciklama.Name = "layoutControlItemAciklama";
            this.layoutControlItemAciklama.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemAciklama.Size = new System.Drawing.Size(652, 172);
            this.layoutControlItemAciklama.Text = "Açıklama:";
            this.layoutControlItemAciklama.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlGroupDurum
            // 
            this.layoutControlGroupDurum.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemDurumu,
            this.layoutControlItemRezervMi});
            this.layoutControlGroupDurum.Location = new System.Drawing.Point(0, 198);
            this.layoutControlGroupDurum.Name = "layoutControlGroupDurum";
            this.layoutControlGroupDurum.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 12);
            this.layoutControlGroupDurum.Size = new System.Drawing.Size(676, 52);
            this.layoutControlGroupDurum.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 8, 0);
            this.layoutControlGroupDurum.Text = "Durum Bilgileri";
            // 
            // layoutControlItemDurumu
            // 
            this.layoutControlItemDurumu.Control = this.checkEditDurumu;
            this.layoutControlItemDurumu.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemDurumu.Name = "layoutControlItemDurumu";
            this.layoutControlItemDurumu.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemDurumu.Size = new System.Drawing.Size(652, 20);
            this.layoutControlItemDurumu.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemRezervMi
            // 
            this.layoutControlItemRezervMi.Control = this.checkEditRezervMi;
            this.layoutControlItemRezervMi.Location = new System.Drawing.Point(0, 20);
            this.layoutControlItemRezervMi.Name = "layoutControlItemRezervMi";
            this.layoutControlItemRezervMi.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemRezervMi.Size = new System.Drawing.Size(652, 20);
            this.layoutControlItemRezervMi.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlGroupTarih
            // 
            this.layoutControlGroupTarih.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemEklenmeTarihi,
            this.layoutControlItemSonIslemTarihi});
            this.layoutControlGroupTarih.Location = new System.Drawing.Point(0, 250);
            this.layoutControlGroupTarih.Name = "layoutControlGroupTarih";
            this.layoutControlGroupTarih.Size = new System.Drawing.Size(676, 128);
            this.layoutControlGroupTarih.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 8, 0);
            this.layoutControlGroupTarih.Text = "Tarih Bilgileri";
            // 
            // layoutControlItemEklenmeTarihi
            // 
            this.layoutControlItemEklenmeTarihi.Control = this.dateEditEklenmeTarihi;
            this.layoutControlItemEklenmeTarihi.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemEklenmeTarihi.Name = "layoutControlItemEklenmeTarihi";
            this.layoutControlItemEklenmeTarihi.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemEklenmeTarihi.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemEklenmeTarihi.Text = "Eklenme Tarihi:";
            this.layoutControlItemEklenmeTarihi.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemSonIslemTarihi
            // 
            this.layoutControlItemSonIslemTarihi.Control = this.dateEditSonIslemTarihi;
            this.layoutControlItemSonIslemTarihi.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemSonIslemTarihi.Name = "layoutControlItemSonIslemTarihi";
            this.layoutControlItemSonIslemTarihi.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemSonIslemTarihi.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemSonIslemTarihi.Text = "Son İşlem Tarihi:";
            this.layoutControlItemSonIslemTarihi.TextSize = new System.Drawing.Size(96, 15);
            // 
            // frmMasaKaydet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 520);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMasaKaydet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Masa Kayıt";
            this.Load += new System.EventHandler(this.frmMasaKaydet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSonIslemTarihi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSonIslemTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEklenmeTarihi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEklenmeTarihi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditRezervMi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditDurumu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMasaAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTemel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMasaAdi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAciklama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupDurum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDurumu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemRezervMi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTarih)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemEklenmeTarihi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSonIslemTarihi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraEditors.SimpleButton btnMasaKaydet;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtMasaAdi;
        private DevExpress.XtraEditors.MemoEdit txtAciklama;
        private DevExpress.XtraEditors.CheckEdit checkEditDurumu;
        private DevExpress.XtraEditors.CheckEdit checkEditRezervMi;
        private DevExpress.XtraEditors.DateEdit dateEditEklenmeTarihi;
        private DevExpress.XtraEditors.DateEdit dateEditSonIslemTarihi;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTemel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMasaAdi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAciklama;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupDurum;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDurumu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRezervMi;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTarih;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemEklenmeTarihi;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSonIslemTarihi;
    }
}
