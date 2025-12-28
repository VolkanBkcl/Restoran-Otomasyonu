namespace RestoranOtomasyonu.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestoranOtomasyonu.Entities.Models.RestoranContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RestoranOtomasyonu.Entities.Models.RestoranContext";
        }

        protected override void Seed(RestoranOtomasyonu.Entities.Models.RestoranContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
