namespace RestoranOtomasyonu.WinForms.AnaMenu
{
    partial class frmAnaMenu
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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.Animation.PushTransition pushTransition1 = new DevExpress.Utils.Animation.PushTransition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnaMenu));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barWorkspaceMenuItem1 = new DevExpress.XtraBars.BarWorkspaceMenuItem();
            this.workspaceManager1 = new DevExpress.Utils.WorkspaceManager(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMasalar = new DevExpress.XtraBars.BarButtonItem();
            this.btnMasaHareketleri = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenuler = new DevExpress.XtraBars.BarButtonItem();
            this.M = new DevExpress.XtraBars.BarButtonItem();
            this.btnMenuHareketleri = new DevExpress.XtraBars.BarButtonItem();
            this.btnUrunler = new DevExpress.XtraBars.BarButtonItem();
            this.btnUrunHareketleri = new DevExpress.XtraBars.BarButtonItem();
            this.btnKullanicilar = new DevExpress.XtraBars.BarButtonItem();
            this.btnKullaniciHareketleri = new DevExpress.XtraBars.BarButtonItem();
            this.btnRoller = new DevExpress.XtraBars.BarButtonItem();
            this.btnDoviz = new DevExpress.XtraBars.BarButtonItem();
            this.btnYardim = new DevExpress.XtraBars.BarButtonItem();
            this.btnHakkimizda = new DevExpress.XtraBars.BarButtonItem();
            this.frmRestoran = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.MasalarRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.MenulerRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.UrunlerRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.KullanıcılarRibbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.frmAyarlar = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.AyarlarribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.barWorkspaceMenuItem1,
            this.barButtonItem1,
            this.btnMasalar,
            this.btnMasaHareketleri,
            this.btnMenuler,
            this.M,
            this.btnMenuHareketleri,
            this.btnUrunler,
            this.btnUrunHareketleri,
            this.btnKullanicilar,
            this.btnKullaniciHareketleri,
            this.btnRoller,
            this.btnDoviz,
            this.btnYardim,
            this.btnHakkimizda});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 16;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.frmRestoran,
            this.frmAyarlar});
            this.ribbon.Size = new System.Drawing.Size(1152, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barWorkspaceMenuItem1
            // 
            this.barWorkspaceMenuItem1.Caption = "barWorkspaceMenuItem1";
            this.barWorkspaceMenuItem1.Id = 1;
            this.barWorkspaceMenuItem1.Name = "barWorkspaceMenuItem1";
            // 
            // workspaceManager1
            // 
            this.workspaceManager1.TargetControl = this;
            this.workspaceManager1.TransitionType = pushTransition1;
            this.barWorkspaceMenuItem1.WorkspaceManager = this.workspaceManager1;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btnMasalar
            // 
            this.btnMasalar.Caption = "Masalar";
            this.btnMasalar.Id = 3;
            this.btnMasalar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnMasalar.ImageOptions.SvgImage")));
            this.btnMasalar.Name = "btnMasalar";
            this.btnMasalar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnMasaHareketleri
            // 
            this.btnMasaHareketleri.Caption = "Masa Hareketleri";
            this.btnMasaHareketleri.Id = 4;
            this.btnMasaHareketleri.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnMasaHareketleri.ImageOptions.SvgImage")));
            this.btnMasaHareketleri.Name = "btnMasaHareketleri";
            this.btnMasaHareketleri.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnMenuler
            // 
            this.btnMenuler.Caption = "Menüler";
            this.btnMenuler.Id = 5;
            this.btnMenuler.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnMenuler.ImageOptions.SvgImage")));
            this.btnMenuler.Name = "btnMenuler";
            this.btnMenuler.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // M
            // 
            this.M.Caption = "barButtonItem5";
            this.M.Id = 6;
            this.M.Name = "M";
            // 
            // btnMenuHareketleri
            // 
            this.btnMenuHareketleri.Caption = "Menü Hareketleri";
            this.btnMenuHareketleri.Id = 7;
            this.btnMenuHareketleri.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnMenuHareketleri.ImageOptions.SvgImage")));
            this.btnMenuHareketleri.Name = "btnMenuHareketleri";
            this.btnMenuHareketleri.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnUrunler
            // 
            this.btnUrunler.Caption = "Ürünler";
            this.btnUrunler.Id = 8;
            this.btnUrunler.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUrunler.ImageOptions.SvgImage")));
            this.btnUrunler.Name = "btnUrunler";
            this.btnUrunler.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnUrunler.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUrunler_ItemClick);
            // 
            // btnUrunHareketleri
            // 
            this.btnUrunHareketleri.Caption = "Ürün Hareketleri";
            this.btnUrunHareketleri.Id = 9;
            this.btnUrunHareketleri.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUrunHareketleri.ImageOptions.SvgImage")));
            this.btnUrunHareketleri.Name = "btnUrunHareketleri";
            this.btnUrunHareketleri.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnKullanicilar
            // 
            this.btnKullanicilar.Caption = "Kullanıcılar";
            this.btnKullanicilar.Id = 10;
            this.btnKullanicilar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnKullanicilar.ImageOptions.LargeImage")));
            this.btnKullanicilar.Name = "btnKullanicilar";
            this.btnKullanicilar.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnKullaniciHareketleri
            // 
            this.btnKullaniciHareketleri.Caption = "Kullanıcı Hareketleri";
            this.btnKullaniciHareketleri.Id = 11;
            this.btnKullaniciHareketleri.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnKullaniciHareketleri.ImageOptions.LargeImage")));
            this.btnKullaniciHareketleri.Name = "btnKullaniciHareketleri";
            this.btnKullaniciHareketleri.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnRoller
            // 
            this.btnRoller.Caption = "Rol Tanımları";
            this.btnRoller.Id = 12;
            this.btnRoller.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRoller.ImageOptions.LargeImage")));
            this.btnRoller.Name = "btnRoller";
            this.btnRoller.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnDoviz
            // 
            this.btnDoviz.Caption = "DövizIslemleri";
            this.btnDoviz.Id = 13;
            this.btnDoviz.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDoviz.ImageOptions.LargeImage")));
            this.btnDoviz.Name = "btnDoviz";
            this.btnDoviz.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnYardim
            // 
            this.btnYardim.Caption = "Yardım";
            this.btnYardim.Id = 14;
            this.btnYardim.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnYardim.ImageOptions.SvgImage")));
            this.btnYardim.Name = "btnYardim";
            this.btnYardim.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnHakkimizda
            // 
            this.btnHakkimizda.Caption = "Hakkımızda";
            this.btnHakkimizda.Id = 15;
            this.btnHakkimizda.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnHakkimizda.ImageOptions.SvgImage")));
            this.btnHakkimizda.Name = "btnHakkimizda";
            this.btnHakkimizda.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // frmRestoran
            // 
            this.frmRestoran.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.MasalarRibbonPageGroup,
            this.MenulerRibbonPageGroup,
            this.UrunlerRibbonPageGroup,
            this.KullanıcılarRibbonPageGroup});
            this.frmRestoran.Name = "frmRestoran";
            this.frmRestoran.Text = "Restoran İşlemleri";
            // 
            // MasalarRibbonPageGroup
            // 
            this.MasalarRibbonPageGroup.ItemLinks.Add(this.btnMasalar);
            this.MasalarRibbonPageGroup.ItemLinks.Add(this.btnMasaHareketleri);
            this.MasalarRibbonPageGroup.Name = "MasalarRibbonPageGroup";
            this.MasalarRibbonPageGroup.Text = "Masalar";
            // 
            // MenulerRibbonPageGroup
            // 
            this.MenulerRibbonPageGroup.ItemLinks.Add(this.btnMenuler);
            this.MenulerRibbonPageGroup.ItemLinks.Add(this.btnMenuHareketleri);
            this.MenulerRibbonPageGroup.Name = "MenulerRibbonPageGroup";
            this.MenulerRibbonPageGroup.Text = "Menüler";
            // 
            // UrunlerRibbonPageGroup
            // 
            this.UrunlerRibbonPageGroup.ItemLinks.Add(this.btnUrunler);
            this.UrunlerRibbonPageGroup.ItemLinks.Add(this.btnUrunHareketleri);
            this.UrunlerRibbonPageGroup.Name = "UrunlerRibbonPageGroup";
            this.UrunlerRibbonPageGroup.Text = "Ürünler";
            // 
            // KullanıcılarRibbonPageGroup
            // 
            this.KullanıcılarRibbonPageGroup.ItemLinks.Add(this.btnKullanicilar);
            this.KullanıcılarRibbonPageGroup.ItemLinks.Add(this.btnKullaniciHareketleri);
            this.KullanıcılarRibbonPageGroup.ItemLinks.Add(this.btnRoller);
            this.KullanıcılarRibbonPageGroup.Name = "KullanıcılarRibbonPageGroup";
            this.KullanıcılarRibbonPageGroup.Text = "Kullanıcılar";
            // 
            // frmAyarlar
            // 
            this.frmAyarlar.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.AyarlarribbonPageGroup});
            this.frmAyarlar.Name = "frmAyarlar";
            this.frmAyarlar.Text = "Ayarlar";
            // 
            // AyarlarribbonPageGroup
            // 
            this.AyarlarribbonPageGroup.ItemLinks.Add(this.btnDoviz);
            this.AyarlarribbonPageGroup.ItemLinks.Add(this.btnYardim);
            this.AyarlarribbonPageGroup.ItemLinks.Add(this.btnHakkimizda);
            this.AyarlarribbonPageGroup.Name = "AyarlarribbonPageGroup";
            this.AyarlarribbonPageGroup.Text = "Ayarlar";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 638);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1152, 24);
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // frmAnaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 662);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Name = "frmAnaMenu";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Restoran Otomasyonu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage frmRestoran;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup MasalarRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarWorkspaceMenuItem barWorkspaceMenuItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup MenulerRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup UrunlerRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup KullanıcılarRibbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPage frmAyarlar;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup AyarlarribbonPageGroup;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnMasalar;
        private DevExpress.XtraBars.BarButtonItem btnMasaHareketleri;
        private DevExpress.XtraBars.BarButtonItem btnMenuler;
        private DevExpress.XtraBars.BarButtonItem M;
        private DevExpress.XtraBars.BarButtonItem btnMenuHareketleri;
        private DevExpress.XtraBars.BarButtonItem btnUrunler;
        private DevExpress.XtraBars.BarButtonItem btnUrunHareketleri;
        private DevExpress.XtraBars.BarButtonItem btnKullanicilar;
        private DevExpress.XtraBars.BarButtonItem btnKullaniciHareketleri;
        private DevExpress.XtraBars.BarButtonItem btnRoller;
        private DevExpress.XtraBars.BarButtonItem btnDoviz;
        private DevExpress.XtraBars.BarButtonItem btnYardim;
        private DevExpress.XtraBars.BarButtonItem btnHakkimizda;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.Utils.WorkspaceManager workspaceManager1;
    }
}