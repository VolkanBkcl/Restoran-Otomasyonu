namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuAdi = c.String(maxLength: 50, unicode: false),
                        Aciklama = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Uurn",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(nullable: false),
                        UrunKodu = c.String(maxLength: 20, unicode: false),
                        UrunAdi = c.String(),
                        BirimFiyati1 = c.String(),
                        BirimFiyati2 = c.String(),
                        Aciklama = c.String(),
                        Tarih = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uurn", "MenuId", "dbo.Menu");
            DropIndex("dbo.Uurn", new[] { "MenuId" });
            DropTable("dbo.Uurn");
            DropTable("dbo.Menu");
        }
    }
}
