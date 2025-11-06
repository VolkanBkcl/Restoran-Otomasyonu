namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UrunResim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Urun", "Resim", c => c.String());
            DropColumn("dbo.MasaHareketleri", "Resim");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MasaHareketleri", "Resim", c => c.String());
            DropColumn("dbo.Urun", "Resim");
        }
    }
}
