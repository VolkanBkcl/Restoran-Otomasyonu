namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MasaAdiSilindi : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            AddColumn("dbo.Urun", "MasaAdi", c => c.String(maxLength: 15, unicode: false));
        }
    }
}
