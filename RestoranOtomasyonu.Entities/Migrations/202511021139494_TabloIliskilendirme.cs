namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabloIliskilendirme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KullaniciHareketleri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanicilar", t => t.KullaniciId, cascadeDelete: true)
                .Index(t => t.KullaniciId);
            
            CreateTable(
                "dbo.Kullanicilar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(maxLength: 150, unicode: false),
                        Telefon = c.String(maxLength: 15, unicode: false),
                        Adres = c.String(maxLength: 500, unicode: false),
                        Email = c.String(maxLength: 150, unicode: false),
                        Gorevi = c.String(maxLength: 50, unicode: false),
                        KullaniciAdi = c.String(maxLength: 50, unicode: false),
                        Parola = c.String(maxLength: 20, unicode: false),
                        HatirlatmaSorusu = c.String(maxLength: 150, unicode: false),
                        Cevap = c.String(maxLength: 50, unicode: false),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        KayitTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MasaHareketleri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SatisKodu = c.String(maxLength: 15, unicode: false),
                        MasaId = c.Int(nullable: false),
                        Miktari = c.Int(nullable: false),
                        BirimMiktarı = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Masalar", t => t.MasaId, cascadeDelete: true)
                .Index(t => t.MasaId);
            
            CreateTable(
                "dbo.Masalar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasaAdi = c.String(maxLength: 15, unicode: false),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        Durumu = c.Boolean(nullable: false),
                        RezervMi = c.Boolean(nullable: false),
                        EklenmeTarihi = c.DateTime(nullable: false, storeType: "date"),
                        SonİslemTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuAdi = c.String(maxLength: 60, unicode: false),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Urun",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(nullable: false),
                        MasaAdi = c.String(maxLength: 15, unicode: false),
                        UrunAdi = c.String(maxLength: 50, unicode: false),
                        BirimFiyati1 = c.Decimal(nullable: false, precision: 28, scale: 2),
                        BirimFiyati2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        Tarih = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.OdemeHareketleri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SatisKodu = c.String(maxLength: 15, unicode: false),
                        OdemeTuru = c.String(maxLength: 50, unicode: false),
                        Odenen = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roller",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        FormName = c.String(maxLength: 50, unicode: false),
                        ControlName = c.String(maxLength: 50, unicode: false),
                        ControlCaption = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Satislar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SatisKodu = c.String(maxLength: 15, unicode: false),
                        Tutar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Odenen = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Kalan = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        SonİslemTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Urun", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.MasaHareketleri", "MasaId", "dbo.Masalar");
            DropForeignKey("dbo.KullaniciHareketleri", "KullaniciId", "dbo.Kullanicilar");
            DropIndex("dbo.Urun", new[] { "MenuId" });
            DropIndex("dbo.MasaHareketleri", new[] { "MasaId" });
            DropIndex("dbo.KullaniciHareketleri", new[] { "KullaniciId" });
            DropTable("dbo.Satislar");
            DropTable("dbo.Roller");
            DropTable("dbo.OdemeHareketleri");
            DropTable("dbo.Urun");
            DropTable("dbo.Menu");
            DropTable("dbo.Masalar");
            DropTable("dbo.MasaHareketleri");
            DropTable("dbo.Kullanicilar");
            DropTable("dbo.KullaniciHareketleri");
        }
    }
}
