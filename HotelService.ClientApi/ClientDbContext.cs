using HotelService.ClientApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelService.ClientApi
{
    public class ClientDbContext :DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Client> Clients { get; set; }


        public ClientDbContext(IConfiguration configuration)
            :base()
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=Hotel;trusted_connection=true;",
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Hotel"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
