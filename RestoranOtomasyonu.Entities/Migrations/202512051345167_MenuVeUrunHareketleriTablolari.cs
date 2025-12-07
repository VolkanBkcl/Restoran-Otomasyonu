namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuVeUrunHareketleriTablolari : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuHareketleri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SatisKodu = c.String(maxLength: 15, unicode: false),
                        MenuId = c.Int(nullable: false),
                        Miktari = c.Int(nullable: false),
                        BirimMiktarı = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BirimFiyati = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.UrunHareketleri",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SatisKodu = c.String(maxLength: 15, unicode: false),
                        UrunId = c.Int(nullable: false),
                        Miktari = c.Int(nullable: false),
                        BirimMiktarı = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BirimFiyati = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Aciklama = c.String(maxLength: 300, unicode: false),
                        Tarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Urun", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UrunHareketleri", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.MenuHareketleri", "MenuId", "dbo.Menu");
            DropIndex("dbo.UrunHareketleri", new[] { "UrunId" });
            DropIndex("dbo.MenuHareketleri", new[] { "MenuId" });
            DropTable("dbo.UrunHareketleri");
            DropTable("dbo.MenuHareketleri");
        }
    }
}
