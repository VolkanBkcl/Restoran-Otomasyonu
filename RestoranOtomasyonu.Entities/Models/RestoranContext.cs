using RestoranOtomasyonu.Entities.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RestoranOtomasyonu.Entities.Models
{
    public class RestoranContext:DbContext
    {
        static RestoranContext()
        {
            // Migration geçmişini okurken XML hatalarını önlemek için
            // Migration initializer'ı devre dışı bırak
            Database.SetInitializer<RestoranContext>(null);
        }

        public RestoranContext():base("name=connection")
        {
            // Migration geçmişini okurken XML hatalarını önlemek için
            // Configuration'ı yüklerken hata oluşmasını engelle
            try
            {
                // Veritabanının var olup olmadığını kontrol et
                if (!Database.Exists())
                {
                    Database.CreateIfNotExists();
                }
            }
            catch (XmlException)
            {
                // XML hatası oluşursa, migration geçmişini atla
                // Bu durumda veritabanı zaten var olduğu için devam edebiliriz
            }
            catch
            {
                // Diğer hatalar için de devam et
            }
        }

        public DbSet<Menu> Menu { get; set; }

        public DbSet<Urun> Urun { get; set; }

        public DbSet<KullaniciHareketleri> KullaniciHareketleri { get; set; }

        public DbSet<Kullanicilar> Kullanicilar { get; set; }

        public DbSet<MasaHareketleri> MasaHareketleri { get; set; }

        public DbSet<MenuHareketleri> MenuHareketleri { get; set; }

        public DbSet<UrunHareketleri> UrunHareketleri { get; set; }

        public DbSet<Masalar> Masalar { get; set; }

        public DbSet<OdemeHareketleri> OdemeHareketleri { get; set; }

        public DbSet<Roller> Roller { get; set; }

        public DbSet<Satislar> Satislar { get; set; }

        public DbSet<Siparisler> Siparisler { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new KullaniciHareketleriMap());
            modelBuilder.Configurations.Add(new KullanicilarMap());
            modelBuilder.Configurations.Add(new MasaHareketleriMap());
            modelBuilder.Configurations.Add(new MasalarMap());
            modelBuilder.Configurations.Add(new OdemeHareketleriMap());
            modelBuilder.Configurations.Add(new RollerMap());
            modelBuilder.Configurations.Add(new SatislarMap());
            modelBuilder.Configurations.Add(new UrunMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new MenuHareketleriMap());
            modelBuilder.Configurations.Add(new UrunHareketleriMap());
            modelBuilder.Configurations.Add(new SiparislerMap());
        }


    }
}
    