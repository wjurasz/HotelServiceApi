using HotelService.Reservation.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HotelService.Reservation.Storage.Context
{
    public class ReservationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Entities.Reservation> Reservations { get; set; }

        public ReservationDbContext(IConfiguration configuration)
            : base()
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=HotelReservation;trusted_connection=true;",
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "HotelReservation"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}