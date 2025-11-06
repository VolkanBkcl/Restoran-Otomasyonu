namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasaHareketleri", "Resim", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasaHareketleri", "Resim");
        }
    }
}
