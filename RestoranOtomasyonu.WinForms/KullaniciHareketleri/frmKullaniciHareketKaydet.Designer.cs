namespace RestoranOtomasyonu.WinForms.KullaniciHareketleri
{
    partial class frmKullaniciHareketKaydet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKullaniciHareketKaydet));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.btnKullaniciHareketKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dateEditTarih = new DevExpress.XtraEditors.DateEdit();
            this.txtAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.lookUpKullanici = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupTemel = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemKullanici = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAciklama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTarih = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpKullanici.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTemel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKullanici)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAciklama)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTarih)).BeginInit();
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
            this.labelControl1.Text = "Kullanıcı Hareket Kayıt";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnKapat);
            this.groupControl1.Controls.Add(this.btnKullaniciHareketKaydet);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 350);
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
            // btnKullaniciHareketKaydet
            // 
            this.btnKullaniciHareketKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKullaniciHareketKaydet.Appearance.Options.UseFont = true;
            this.btnKullaniciHareketKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKullaniciHareketKaydet.ImageOptions.Image")));
            this.btnKullaniciHareketKaydet.Location = new System.Drawing.Point(24, 24);
            this.btnKullaniciHareketKaydet.Name = "btnKullaniciHareketKaydet";
            this.btnKullaniciHareketKaydet.Size = new System.Drawing.Size(120, 40);
            this.btnKullaniciHareketKaydet.TabIndex = 0;
            this.btnKullaniciHareketKaydet.Text = "Kaydet";
            this.btnKullaniciHareketKaydet.Click += new System.EventHandler(this.btnKullaniciHareketKaydet_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dateEditTarih);
            this.layoutControl1.Controls.Add(this.txtAciklama);
            this.layoutControl1.Controls.Add(this.lookUpKullanici);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 60);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1024, 0, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(700, 290);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dateEditTarih
            // 
            this.dateEditTarih.EditValue = null;
            this.dateEditTarih.Location = new System.Drawing.Point(140, 254);
            this.dateEditTarih.Name = "dateEditTarih";
            this.dateEditTarih.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateEditTarih.Properties.Appearance.Options.UseFont = true;
            this.dateEditTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTarih.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTarih.Size = new System.Drawing.Size(536, 22);
            this.dateEditTarih.StyleController = this.layoutControl1;
            this.dateEditTarih.TabIndex = 11;
            // 
            // txtAciklama
            // 
            this.txtAciklama.Location = new System.Drawing.Point(140, 54);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAciklama.Properties.Appearance.Options.UseFont = true;
            this.txtAciklama.Size = new System.Drawing.Size(536, 196);
            this.txtAciklama.StyleController = this.layoutControl1;
            this.txtAciklama.TabIndex = 9;
            // 
            // lookUpKullanici
            // 
            this.lookUpKullanici.Location = new System.Drawing.Point(140, 28);
            this.lookUpKullanici.Name = "lookUpKullanici";
            this.lookUpKullanici.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lookUpKullanici.Properties.Appearance.Options.UseFont = true;
            this.lookUpKullanici.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpKullanici.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AdSoyad", "Ad Soyad")});
            this.lookUpKullanici.Properties.DisplayMember = "AdSoyad";
            this.lookUpKullanici.Properties.NullText = "Kullanıcı Seçiniz";
            this.lookUpKullanici.Properties.ValueMember = "Id";
            this.lookUpKullanici.Size = new System.Drawing.Size(536, 22);
            this.lookUpKullanici.StyleController = this.layoutControl1;
            this.lookUpKullanici.TabIndex = 12;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupTemel});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(12, 12, 12, 12);
            this.layoutControlGroup1.Size = new System.Drawing.Size(700, 290);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupTemel
            // 
            this.layoutControlGroupTemel.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemKullanici,
            this.layoutControlItemAciklama,
            this.layoutControlItemTarih});
            this.layoutControlGroupTemel.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupTemel.Name = "layoutControlGroupTemel";
            this.layoutControlGroupTemel.Size = new System.Drawing.Size(676, 266);
            this.layoutControlGroupTemel.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 8, 0);
            this.layoutControlGroupTemel.Text = "Hareket Bilgileri";
            // 
            // layoutControlItemKullanici
            // 
            this.layoutControlItemKullanici.Control = this.lookUpKullanici;
            this.layoutControlItemKullanici.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemKullanici.Name = "layoutControlItemKullanici";
            this.layoutControlItemKullanici.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemKullanici.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemKullanici.Text = "Kullanıcı:";
            this.layoutControlItemKullanici.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemAciklama
            // 
            this.layoutControlItemAciklama.Control = this.txtAciklama;
            this.layoutControlItemAciklama.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemAciklama.Name = "layoutControlItemAciklama";
            this.layoutControlItemAciklama.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemAciklama.Size = new System.Drawing.Size(652, 204);
            this.layoutControlItemAciklama.Text = "Açıklama:";
            this.layoutControlItemAciklama.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemTarih
            // 
            this.layoutControlItemTarih.Control = this.dateEditTarih;
            this.layoutControlItemTarih.Location = new System.Drawing.Point(0, 230);
            this.layoutControlItemTarih.Name = "layoutControlItemTarih";
            this.layoutControlItemTarih.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemTarih.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemTarih.Text = "Tarih:";
            this.layoutControlItemTarih.TextSize = new System.Drawing.Size(96, 15);
            // 
            // frmKullaniciHareketKaydet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 420);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKullaniciHareketKaydet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kullanıcı Hareket Kayıt";
            this.Load += new System.EventHandler(this.frmKullaniciHareketKaydet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpKullanici.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTemel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKullanici)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAciklama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTarih)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraEditors.SimpleButton btnKullaniciHareketKaydet;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpKullanici;
        private DevExpress.XtraEditors.MemoEdit txtAciklama;
        private DevExpress.XtraEditors.DateEdit dateEditTarih;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTemel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKullanici;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAciklama;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTarih;
    }
}
