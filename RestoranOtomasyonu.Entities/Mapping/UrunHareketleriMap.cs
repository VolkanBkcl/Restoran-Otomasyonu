using RestoranOtomasyonu.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Mapping
{
    public class UrunHareketleriMap:EntityTypeConfiguration<UrunHareketleri>
    {
        public UrunHareketleriMap()
        {
            this.ToTable("UrunHareketleri");
            this.HasKey(p => p.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.SatisKodu).HasColumnType("varchar").HasMaxLength(15);
            this.Property(p => p.Aciklama).HasColumnType("varchar").HasMaxLength(300);
            this.Property(p => p.BirimMiktarı).HasColumnType("decimal").HasPrecision(18, 2);
            this.Property(p => p.BirimFiyati).HasColumnType("decimal").HasPrecision(18, 2);

            this.HasRequired(x => x.Urun).WithMany().HasForeignKey(x => x.UrunId);

        }
    }
}







