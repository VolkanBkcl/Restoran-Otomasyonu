namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MasaHareketlerindeDeğişiklik : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasaHareketleri", "MenuId", c => c.Int(nullable: false));
            AddColumn("dbo.MasaHareketleri", "UrunId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasaHareketleri", "UrunId");
            DropColumn("dbo.MasaHareketleri", "MenuId");
        }
    }
}
