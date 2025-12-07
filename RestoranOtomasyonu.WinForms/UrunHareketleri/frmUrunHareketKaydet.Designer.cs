namespace RestoranOtomasyonu.WinForms.UrunHareketleri
{
    partial class frmUrunHareketKaydet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUrunHareketKaydet));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.btnUrunHareketKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dateEditTarih = new DevExpress.XtraEditors.DateEdit();
            this.txtAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.calcBirimFiyati = new DevExpress.XtraEditors.CalcEdit();
            this.calcBirimMiktari = new DevExpress.XtraEditors.CalcEdit();
            this.calcMiktari = new DevExpress.XtraEditors.CalcEdit();
            this.lookUpUrun = new DevExpress.XtraEditors.LookUpEdit();
            this.txtSatisKodu = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupTemel = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemSatisKodu = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemUrun = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroupMiktarFiyat = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemMiktari = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBirimMiktari = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemBirimFiyati = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAciklama = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemTarih = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcBirimFiyati.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcBirimMiktari.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcMiktari.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpUrun.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSatisKodu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTemel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSatisKodu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUrun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMiktarFiyat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMiktari)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBirimMiktari)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBirimFiyati)).BeginInit();
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
            this.labelControl1.Text = "Ürün Hareket Kayıt";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnKapat);
            this.groupControl1.Controls.Add(this.btnUrunHareketKaydet);
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
            // btnUrunHareketKaydet
            // 
            this.btnUrunHareketKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnUrunHareketKaydet.Appearance.Options.UseFont = true;
            this.btnUrunHareketKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUrunHareketKaydet.ImageOptions.Image")));
            this.btnUrunHareketKaydet.Location = new System.Drawing.Point(24, 24);
            this.btnUrunHareketKaydet.Name = "btnUrunHareketKaydet";
            this.btnUrunHareketKaydet.Size = new System.Drawing.Size(120, 40);
            this.btnUrunHareketKaydet.TabIndex = 0;
            this.btnUrunHareketKaydet.Text = "Kaydet";
            this.btnUrunHareketKaydet.Click += new System.EventHandler(this.btnUrunHareketKaydet_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dateEditTarih);
            this.layoutControl1.Controls.Add(this.txtAciklama);
            this.layoutControl1.Controls.Add(this.calcBirimFiyati);
            this.layoutControl1.Controls.Add(this.calcBirimMiktari);
            this.layoutControl1.Controls.Add(this.calcMiktari);
            this.layoutControl1.Controls.Add(this.lookUpUrun);
            this.layoutControl1.Controls.Add(this.txtSatisKodu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 60);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1024, 0, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(700, 390);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dateEditTarih
            // 
            this.dateEditTarih.EditValue = null;
            this.dateEditTarih.Location = new System.Drawing.Point(140, 324);
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
            this.txtAciklama.Location = new System.Drawing.Point(140, 198);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAciklama.Properties.Appearance.Options.UseFont = true;
            this.txtAciklama.Size = new System.Drawing.Size(536, 122);
            this.txtAciklama.StyleController = this.layoutControl1;
            this.txtAciklama.TabIndex = 9;
            // 
            // calcBirimFiyati
            // 
            this.calcBirimFiyati.Location = new System.Drawing.Point(140, 172);
            this.calcBirimFiyati.Name = "calcBirimFiyati";
            this.calcBirimFiyati.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.calcBirimFiyati.Properties.Appearance.Options.UseFont = true;
            this.calcBirimFiyati.Properties.Appearance.Options.UseTextOptions = true;
            this.calcBirimFiyati.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calcBirimFiyati.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcBirimFiyati.Properties.DisplayFormat.FormatString = "C2";
            this.calcBirimFiyati.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.calcBirimFiyati.Size = new System.Drawing.Size(536, 22);
            this.calcBirimFiyati.StyleController = this.layoutControl1;
            this.calcBirimFiyati.TabIndex = 8;
            // 
            // calcBirimMiktari
            // 
            this.calcBirimMiktari.Location = new System.Drawing.Point(140, 146);
            this.calcBirimMiktari.Name = "calcBirimMiktari";
            this.calcBirimMiktari.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.calcBirimMiktari.Properties.Appearance.Options.UseFont = true;
            this.calcBirimMiktari.Properties.Appearance.Options.UseTextOptions = true;
            this.calcBirimMiktari.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calcBirimMiktari.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcBirimMiktari.Properties.DisplayFormat.FormatString = "N2";
            this.calcBirimMiktari.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.calcBirimMiktari.Size = new System.Drawing.Size(536, 22);
            this.calcBirimMiktari.StyleController = this.layoutControl1;
            this.calcBirimMiktari.TabIndex = 7;
            // 
            // calcMiktari
            // 
            this.calcMiktari.Location = new System.Drawing.Point(140, 120);
            this.calcMiktari.Name = "calcMiktari";
            this.calcMiktari.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.calcMiktari.Properties.Appearance.Options.UseFont = true;
            this.calcMiktari.Properties.Appearance.Options.UseTextOptions = true;
            this.calcMiktari.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.calcMiktari.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcMiktari.Properties.DisplayFormat.FormatString = "N0";
            this.calcMiktari.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.calcMiktari.Size = new System.Drawing.Size(536, 22);
            this.calcMiktari.StyleController = this.layoutControl1;
            this.calcMiktari.TabIndex = 6;
            // 
            // lookUpUrun
            // 
            this.lookUpUrun.Location = new System.Drawing.Point(140, 54);
            this.lookUpUrun.Name = "lookUpUrun";
            this.lookUpUrun.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lookUpUrun.Properties.Appearance.Options.UseFont = true;
            this.lookUpUrun.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpUrun.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UrunAdi", "Ürün Adı")});
            this.lookUpUrun.Properties.DisplayMember = "UrunAdi";
            this.lookUpUrun.Properties.NullText = "Ürün Seçiniz";
            this.lookUpUrun.Properties.ValueMember = "Id";
            this.lookUpUrun.Size = new System.Drawing.Size(536, 22);
            this.lookUpUrun.StyleController = this.layoutControl1;
            this.lookUpUrun.TabIndex = 5;
            // 
            // txtSatisKodu
            // 
            this.txtSatisKodu.Location = new System.Drawing.Point(140, 28);
            this.txtSatisKodu.Name = "txtSatisKodu";
            this.txtSatisKodu.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSatisKodu.Properties.Appearance.Options.UseFont = true;
            this.txtSatisKodu.Size = new System.Drawing.Size(536, 22);
            this.txtSatisKodu.StyleController = this.layoutControl1;
            this.txtSatisKodu.TabIndex = 2;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupTemel,
            this.layoutControlGroupMiktarFiyat,
            this.layoutControlItemAciklama,
            this.layoutControlItemTarih});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(12, 12, 12, 12);
            this.layoutControlGroup1.Size = new System.Drawing.Size(700, 390);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupTemel
            // 
            this.layoutControlGroupTemel.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemSatisKodu,
            this.layoutControlItemUrun});
            this.layoutControlGroupTemel.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupTemel.Name = "layoutControlGroupTemel";
            this.layoutControlGroupTemel.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 12);
            this.layoutControlGroupTemel.Size = new System.Drawing.Size(676, 52);
            this.layoutControlGroupTemel.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 8, 0);
            this.layoutControlGroupTemel.Text = "Temel Bilgiler";
            // 
            // layoutControlItemSatisKodu
            // 
            this.layoutControlItemSatisKodu.Control = this.txtSatisKodu;
            this.layoutControlItemSatisKodu.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemSatisKodu.Name = "layoutControlItemSatisKodu";
            this.layoutControlItemSatisKodu.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemSatisKodu.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemSatisKodu.Text = "Satış Kodu:";
            this.layoutControlItemSatisKodu.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemUrun
            // 
            this.layoutControlItemUrun.Control = this.lookUpUrun;
            this.layoutControlItemUrun.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemUrun.Name = "layoutControlItemUrun";
            this.layoutControlItemUrun.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemUrun.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemUrun.Text = "Ürün:";
            this.layoutControlItemUrun.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlGroupMiktarFiyat
            // 
            this.layoutControlGroupMiktarFiyat.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemMiktari,
            this.layoutControlItemBirimMiktari,
            this.layoutControlItemBirimFiyati});
            this.layoutControlGroupMiktarFiyat.Location = new System.Drawing.Point(0, 52);
            this.layoutControlGroupMiktarFiyat.Name = "layoutControlGroupMiktarFiyat";
            this.layoutControlGroupMiktarFiyat.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 12);
            this.layoutControlGroupMiktarFiyat.Size = new System.Drawing.Size(676, 78);
            this.layoutControlGroupMiktarFiyat.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 8, 0);
            this.layoutControlGroupMiktarFiyat.Text = "Miktar ve Fiyat Bilgileri";
            // 
            // layoutControlItemMiktari
            // 
            this.layoutControlItemMiktari.Control = this.calcMiktari;
            this.layoutControlItemMiktari.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemMiktari.Name = "layoutControlItemMiktari";
            this.layoutControlItemMiktari.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemMiktari.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemMiktari.Text = "Miktarı:";
            this.layoutControlItemMiktari.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemBirimMiktari
            // 
            this.layoutControlItemBirimMiktari.Control = this.calcBirimMiktari;
            this.layoutControlItemBirimMiktari.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemBirimMiktari.Name = "layoutControlItemBirimMiktari";
            this.layoutControlItemBirimMiktari.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemBirimMiktari.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemBirimMiktari.Text = "Birim Miktarı:";
            this.layoutControlItemBirimMiktari.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemBirimFiyati
            // 
            this.layoutControlItemBirimFiyati.Control = this.calcBirimFiyati;
            this.layoutControlItemBirimFiyati.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItemBirimFiyati.Name = "layoutControlItemBirimFiyati";
            this.layoutControlItemBirimFiyati.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemBirimFiyati.Size = new System.Drawing.Size(652, 26);
            this.layoutControlItemBirimFiyati.Text = "Birim Fiyatı:";
            this.layoutControlItemBirimFiyati.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemAciklama
            // 
            this.layoutControlItemAciklama.Control = this.txtAciklama;
            this.layoutControlItemAciklama.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItemAciklama.Name = "layoutControlItemAciklama";
            this.layoutControlItemAciklama.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemAciklama.Size = new System.Drawing.Size(676, 138);
            this.layoutControlItemAciklama.Text = "Açıklama:";
            this.layoutControlItemAciklama.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemTarih
            // 
            this.layoutControlItemTarih.Control = this.dateEditTarih;
            this.layoutControlItemTarih.Location = new System.Drawing.Point(0, 268);
            this.layoutControlItemTarih.Name = "layoutControlItemTarih";
            this.layoutControlItemTarih.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemTarih.Size = new System.Drawing.Size(676, 26);
            this.layoutControlItemTarih.Text = "Tarih:";
            this.layoutControlItemTarih.TextSize = new System.Drawing.Size(96, 15);
            // 
            // frmUrunHareketKaydet
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
            this.Name = "frmUrunHareketKaydet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ürün Hareket Kayıt";
            this.Load += new System.EventHandler(this.frmUrunHareketKaydet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcBirimFiyati.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcBirimMiktari.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcMiktari.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpUrun.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSatisKodu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupTemel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemSatisKodu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemUrun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupMiktarFiyat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemMiktari)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBirimMiktari)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBirimFiyati)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAciklama)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTarih)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraEditors.SimpleButton btnUrunHareketKaydet;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtSatisKodu;
        private DevExpress.XtraEditors.LookUpEdit lookUpUrun;
        private DevExpress.XtraEditors.CalcEdit calcMiktari;
        private DevExpress.XtraEditors.CalcEdit calcBirimMiktari;
        private DevExpress.XtraEditors.CalcEdit calcBirimFiyati;
        private DevExpress.XtraEditors.MemoEdit txtAciklama;
        private DevExpress.XtraEditors.DateEdit dateEditTarih;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTemel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemSatisKodu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemUrun;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupMiktarFiyat;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemMiktari;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBirimMiktari;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBirimFiyati;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAciklama;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTarih;
    }
}
