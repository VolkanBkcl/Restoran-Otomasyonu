namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BirimFiyatGüncellemesi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Urun", "BirimFiyati3", c => c.Decimal(nullable: false, precision: 28, scale: 2));
            AlterColumn("dbo.Urun", "BirimFiyati2", c => c.Decimal(nullable: false, precision: 28, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Urun", "BirimFiyati2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Urun", "BirimFiyati3");
        }
    }
}
