namespace RestoranOtomasyonu.WinForms.Roller
{
    partial class frmRolKaydet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRolKaydet));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnKapat = new DevExpress.XtraEditors.SimpleButton();
            this.btnRolKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtControlCaption = new DevExpress.XtraEditors.TextEdit();
            this.txtControlName = new DevExpress.XtraEditors.TextEdit();
            this.txtFormName = new DevExpress.XtraEditors.TextEdit();
            this.lookUpKullanici = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroupRol = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemKullanici = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemFormName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemControlName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemControlCaption = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtControlCaption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtControlName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpKullanici.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKullanici)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFormName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemControlName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemControlCaption)).BeginInit();
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
            this.labelControl1.Size = new System.Drawing.Size(600, 60);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Rol Kayıt";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnKapat);
            this.groupControl1.Controls.Add(this.btnRolKaydet);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 280);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Padding = new System.Windows.Forms.Padding(12);
            this.groupControl1.Size = new System.Drawing.Size(600, 70);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "İşlemler";
            // 
            // btnKapat
            // 
            this.btnKapat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnKapat.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKapat.Appearance.Options.UseFont = true;
            this.btnKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKapat.ImageOptions.Image")));
            this.btnKapat.Location = new System.Drawing.Point(490, 24);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(98, 40);
            this.btnKapat.TabIndex = 1;
            this.btnKapat.Text = "İptal";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // btnRolKaydet
            // 
            this.btnRolKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRolKaydet.Appearance.Options.UseFont = true;
            this.btnRolKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRolKaydet.ImageOptions.Image")));
            this.btnRolKaydet.Location = new System.Drawing.Point(24, 24);
            this.btnRolKaydet.Name = "btnRolKaydet";
            this.btnRolKaydet.Size = new System.Drawing.Size(120, 40);
            this.btnRolKaydet.TabIndex = 0;
            this.btnRolKaydet.Text = "Kaydet";
            this.btnRolKaydet.Click += new System.EventHandler(this.btnRolKaydet_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtControlCaption);
            this.layoutControl1.Controls.Add(this.txtControlName);
            this.layoutControl1.Controls.Add(this.txtFormName);
            this.layoutControl1.Controls.Add(this.lookUpKullanici);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 60);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1024, 0, 650, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(600, 220);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtControlCaption
            // 
            this.txtControlCaption.Location = new System.Drawing.Point(140, 106);
            this.txtControlCaption.Name = "txtControlCaption";
            this.txtControlCaption.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtControlCaption.Properties.Appearance.Options.UseFont = true;
            this.txtControlCaption.Size = new System.Drawing.Size(436, 22);
            this.txtControlCaption.StyleController = this.layoutControl1;
            this.txtControlCaption.TabIndex = 7;
            // 
            // txtControlName
            // 
            this.txtControlName.Location = new System.Drawing.Point(140, 80);
            this.txtControlName.Name = "txtControlName";
            this.txtControlName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtControlName.Properties.Appearance.Options.UseFont = true;
            this.txtControlName.Size = new System.Drawing.Size(436, 22);
            this.txtControlName.StyleController = this.layoutControl1;
            this.txtControlName.TabIndex = 6;
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(140, 54);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFormName.Properties.Appearance.Options.UseFont = true;
            this.txtFormName.Size = new System.Drawing.Size(436, 22);
            this.txtFormName.StyleController = this.layoutControl1;
            this.txtFormName.TabIndex = 5;
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
            this.lookUpKullanici.Size = new System.Drawing.Size(436, 22);
            this.lookUpKullanici.StyleController = this.layoutControl1;
            this.lookUpKullanici.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroupRol});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(12, 12, 12, 12);
            this.layoutControlGroup1.Size = new System.Drawing.Size(600, 220);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroupRol
            // 
            this.layoutControlGroupRol.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemKullanici,
            this.layoutControlItemFormName,
            this.layoutControlItemControlName,
            this.layoutControlItemControlCaption});
            this.layoutControlGroupRol.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroupRol.Name = "layoutControlGroupRol";
            this.layoutControlGroupRol.Size = new System.Drawing.Size(576, 196);
            this.layoutControlGroupRol.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 8, 0);
            this.layoutControlGroupRol.Text = "Rol Bilgileri";
            // 
            // layoutControlItemKullanici
            // 
            this.layoutControlItemKullanici.Control = this.lookUpKullanici;
            this.layoutControlItemKullanici.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemKullanici.Name = "layoutControlItemKullanici";
            this.layoutControlItemKullanici.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemKullanici.Size = new System.Drawing.Size(552, 26);
            this.layoutControlItemKullanici.Text = "Kullanıcı:";
            this.layoutControlItemKullanici.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemFormName
            // 
            this.layoutControlItemFormName.Control = this.txtFormName;
            this.layoutControlItemFormName.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItemFormName.Name = "layoutControlItemFormName";
            this.layoutControlItemFormName.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemFormName.Size = new System.Drawing.Size(552, 26);
            this.layoutControlItemFormName.Text = "Form Adı:";
            this.layoutControlItemFormName.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemControlName
            // 
            this.layoutControlItemControlName.Control = this.txtControlName;
            this.layoutControlItemControlName.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItemControlName.Name = "layoutControlItemControlName";
            this.layoutControlItemControlName.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemControlName.Size = new System.Drawing.Size(552, 26);
            this.layoutControlItemControlName.Text = "Kontrol Adı:";
            this.layoutControlItemControlName.TextSize = new System.Drawing.Size(96, 15);
            // 
            // layoutControlItemControlCaption
            // 
            this.layoutControlItemControlCaption.Control = this.txtControlCaption;
            this.layoutControlItemControlCaption.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItemControlCaption.Name = "layoutControlItemControlCaption";
            this.layoutControlItemControlCaption.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItemControlCaption.Size = new System.Drawing.Size(552, 26);
            this.layoutControlItemControlCaption.Text = "Kontrol Başlığı:";
            this.layoutControlItemControlCaption.TextSize = new System.Drawing.Size(96, 15);
            // 
            // frmRolKaydet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 350);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRolKaydet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rol Kayıt";
            this.Load += new System.EventHandler(this.frmRolKaydet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtControlCaption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtControlName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpKullanici.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemKullanici)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFormName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemControlName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemControlCaption)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnKapat;
        private DevExpress.XtraEditors.SimpleButton btnRolKaydet;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpKullanici;
        private DevExpress.XtraEditors.TextEdit txtFormName;
        private DevExpress.XtraEditors.TextEdit txtControlName;
        private DevExpress.XtraEditors.TextEdit txtControlCaption;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRol;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemKullanici;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFormName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemControlName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemControlCaption;
    }
}
