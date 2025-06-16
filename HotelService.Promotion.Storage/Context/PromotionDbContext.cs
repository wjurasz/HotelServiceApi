using HotelService.Promotion.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelService.Promotion.Storage.Context
{
    public class PromotionDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Entities.Promotion> Promotions { get; set; }

        public PromotionDbContext(IConfiguration configuration)
            : base()
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=HotelPromotion;trusted_connection=true;",
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "HotelPromotion"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Promotion>()
                .Property(p => p.DiscountPercentage)
                .HasPrecision(5, 2);
        }
    }
}
