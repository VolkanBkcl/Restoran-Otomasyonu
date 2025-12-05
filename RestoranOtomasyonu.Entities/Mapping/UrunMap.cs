using RestoranOtomasyonu.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.Entities.Mapping
{
    public class UrunMap:EntityTypeConfiguration<Urun>
    {
        public UrunMap() 
        {
            this.ToTable("Urun");
            this.HasKey(p=> p.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.UrunAdi).HasColumnType("varchar").HasMaxLength(50);
            this.Property(p => p.Aciklama).HasColumnType("varchar").HasMaxLength(300);
            this.Property(p => p.BirimFiyati1).HasPrecision(28,2);
            this.Property(p => p.BirimFiyati2).HasPrecision(28,2);
            this.Property(p => p.BirimFiyati3).HasPrecision(28,2);

            this.HasRequired(x => x.Menu).WithMany(x => x.Urun).HasForeignKey(x => x.MenuId);
        }

    }
}
