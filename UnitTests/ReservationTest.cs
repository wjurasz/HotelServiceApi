using HotelService.Reservation.Storage.Context;
using HotelService.Reservation.Storage.Entities;
using HotelService.ReservationApi.Resolvers;
using HotelService.ReservationApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ReservationServiceTests
    {
        private ReservationService GetServiceWithInMemoryDb(out ReservationDbContext context)
        {
            var options = new DbContextOptionsBuilder<ReservationDbContext>()
                .UseInMemoryDatabase(databaseName: "ReservationDbTest")
                .Options;

            context = new ReservationDbContext(null!);
            return new ReservationService(context, new PromotionResolver(new HttpClient()));
        }

        [Fact]
        public async Task Add_ShouldAddReservation()
        {
            var options = new DbContextOptionsBuilder<ReservationDbContext>()
                .UseInMemoryDatabase("AddReservationDb")
                .Options;

            using var context = new ReservationDbContext(null!);
            var service = new ReservationService(context, new PromotionResolver(new HttpClient()));
            var reservation = new Reservation
            {
                ClientId = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                Status = ReservationStatus.Pending
            };

            await service.Add(reservation);

            var added = context.Reservations.FirstOrDefault(r => r.ClientId == 1);
            Assert.NotNull(added);
        }

        [Fact]
        public async Task GetById_ShouldReturnReservation()
        {
            var options = new DbContextOptionsBuilder<ReservationDbContext>()
                .UseInMemoryDatabase("GetByIdReservationDb")
                .Options;

            using var context = new ReservationDbContext(null!);
            var service = new ReservationService(context, new PromotionResolver(new HttpClient()));
            var reservation = new Reservation
            {
                ClientId = 2,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                Status = ReservationStatus.Pending
            };
            context.Reservations.Add(reservation);
            context.SaveChanges();

            var result = await service.GetById(reservation.Id);
            Assert.NotNull(result);
            Assert.Equal(2, result.ClientId);
        }
    }
}
