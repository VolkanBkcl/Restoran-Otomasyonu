namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class API : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Siparisler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasaId = c.Int(nullable: false),
                        KullaniciId = c.Int(nullable: false),
                        SatisKodu = c.String(maxLength: 15, unicode: false),
                        Tutar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IndirimOrani = c.Decimal(nullable: false, precision: 5, scale: 2),
                        NetTutar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OdemeDurumu = c.Int(nullable: false),
                        SiparisDurumu = c.Int(nullable: false),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanicilar", t => t.KullaniciId)
                .ForeignKey("dbo.Masalar", t => t.MasaId)
                .Index(t => t.MasaId)
                .Index(t => t.KullaniciId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Siparisler", "MasaId", "dbo.Masalar");
            DropForeignKey("dbo.Siparisler", "KullaniciId", "dbo.Kullanicilar");
            DropIndex("dbo.Siparisler", new[] { "KullaniciId" });
            DropIndex("dbo.Siparisler", new[] { "MasaId" });
            DropTable("dbo.Siparisler");
        }
    }
}
