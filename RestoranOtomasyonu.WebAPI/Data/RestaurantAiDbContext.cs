using Microsoft.EntityFrameworkCore;
using RestoranOtomasyonu.Entities.Models;
using RestoranOtomasyonu.WebAPI.Models;

namespace RestoranOtomasyonu.WebAPI.Data
{
    public class RestaurantAiDbContext : DbContext
    {
        public RestaurantAiDbContext(DbContextOptions<RestaurantAiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Urun> Urun { get; set; } = null!;
        public DbSet<Menu> Menu { get; set; } = null!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>().ToTable("Urun");
            modelBuilder.Entity<Menu>().ToTable("Menu");

            modelBuilder.Entity<Urun>()
                .HasOne(u => u.Menu)
                .WithMany(m => m.Urun)
                .HasForeignKey(u => u.MenuId);

            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessages");
            modelBuilder.Entity<ChatMessage>()
                .HasKey(c => c.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
