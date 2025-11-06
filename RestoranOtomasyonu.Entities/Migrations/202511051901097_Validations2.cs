namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validations2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasaHareketleri", "BirimFiyati", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Urun", "UrunKodu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Urun", "UrunKodu");
            DropColumn("dbo.MasaHareketleri", "BirimFiyati");
        }
    }
}
