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
    /// <summary>
    /// Siparisler tablosu için Entity Framework Fluent API mapping
    /// </summary>
    public class SiparislerMap : EntityTypeConfiguration<Siparisler>
    {
        public SiparislerMap()
        {
            this.ToTable("Siparisler");
            this.HasKey(p => p.Id);
            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // String alanlar
            this.Property(p => p.SatisKodu).HasColumnType("varchar").HasMaxLength(15);
            this.Property(p => p.Aciklama).HasColumnType("varchar").HasMaxLength(300);

            // Decimal alanlar
            this.Property(p => p.Tutar).HasColumnType("decimal").HasPrecision(18, 2);
            this.Property(p => p.IndirimOrani).HasColumnType("decimal").HasPrecision(5, 2);
            this.Property(p => p.NetTutar).HasColumnType("decimal").HasPrecision(18, 2);

            // Enum alanlar
            this.Property(p => p.OdemeDurumu).IsRequired();
            this.Property(p => p.SiparisDurumu).IsRequired();

            // Foreign Keys
            this.HasRequired(x => x.Masalar)
                .WithMany()
                .HasForeignKey(x => x.MasaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Kullanicilar)
                .WithMany()
                .HasForeignKey(x => x.KullaniciId)
                .WillCascadeOnDelete(false);
        }
    }
}

