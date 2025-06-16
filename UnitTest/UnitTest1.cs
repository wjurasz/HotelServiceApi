using HotelService.ClientApi;
using HotelService.ClientApi.Entities;
using HotelService.ClientApi.Services;
using HotelService.Promotion.Storage.Context;
using HotelService.Promotion.Storage.Entities;
using HotelService.PromotionApi.Services;
using HotelService.Reservation.Storage.Context;
using HotelService.Reservation.Storage.Entities;
using HotelService.ReservationApi.Resolvers;
using HotelService.ReservationApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTest
{
    public class UnitTest1
    {

        //___________RESERVATION_____________
        public class ReservationServiceTests
        {
            private ReservationDbContext CreateDbContext()
            {
                var options = new DbContextOptionsBuilder<ReservationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                var config = new ConfigurationBuilder().Build();
                return new ReservationDbContext(config);
            }

            [Fact]
            public async Task Add_ShouldAddReservation()
            {
                var context = CreateDbContext();
                var mockResolver = new Mock<PromotionResolver>(new HttpClient());
                var service = new ReservationService(context, mockResolver.Object);

                var reservation = new Reservation { ClientId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2) };

                await service.Add(reservation);
                var result = await service.GetAll();

                Assert.Single(result);
            }

            [Fact]
            public async Task Confirm_ShouldSetStatusConfirmed()
            {
                var context = CreateDbContext();
                var mockResolver = new Mock<PromotionResolver>(new HttpClient());
                var service = new ReservationService(context, mockResolver.Object);

                var reservation = new Reservation { ClientId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = ReservationStatus.Pending };
                context.Reservations.Add(reservation);
                await context.SaveChangesAsync();

                await service.Confirm(reservation.Id);
                var updated = await service.GetById(reservation.Id);

                Assert.Equal(ReservationStatus.Confirmed, updated.Status);
            }
        }

        //_________________CLIENT__________________________
        public class ClientServiceTests
        {
            private ClientDbContext CreateDbContext()
            {
                var options = new DbContextOptionsBuilder<ClientDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                var config = new ConfigurationBuilder().Build();
                return new ClientDbContext(config);
            }

            [Fact]
            public async Task Add_ShouldSaveClient()
            {
                var context = CreateDbContext();
                var service = new ClientService(context);

                var client = new Client { FirstName = "Jan", LastName = "Kowalski", Email = "a@a.com", PhoneNumber = "123456789" };
                await service.Add(client);

                var result = await service.Get();
                Assert.Single(result);
            }

            [Fact]
            public async Task Delete_ShouldRemoveClient()
            {
                var context = CreateDbContext();
                var service = new ClientService(context);

                var client = new Client { FirstName = "X", LastName = "Y", Email = "x@y.com", PhoneNumber = "999999999" };
                context.Clients.Add(client);
                await context.SaveChangesAsync();

                await service.Delete(client.Id);
                var all = await service.Get();
                Assert.Empty(all);
            }
        }

        //________________________PROMOTION___________________

        public class PromotionServiceTests
        {
            private PromotionDbContext CreateDbContext()
            {
                var options = new DbContextOptionsBuilder<PromotionDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                var config = new ConfigurationBuilder().Build();
                return new PromotionDbContext(config);
            }

            [Fact]
            public async Task Add_ShouldAddPromotion()
            {
                var context = CreateDbContext();
                var service = new PromotionService(context);

                var promo = new Promotion { Code = "TEST", DiscountPercentage = 10 };
                await service.Add(promo);

                var all = await service.GetAll();
                Assert.Single(all);
            }

            [Fact]
            public async Task GetByCode_ShouldReturnCorrectPromotion()
            {
                var context = CreateDbContext();
                var service = new PromotionService(context);

                var promo = new Promotion { Code = "ABC", DiscountPercentage = 25, ValidUntil = DateTime.Now.AddDays(2) };
                context.Promotions.Add(promo);
                await context.SaveChangesAsync();

                var found = await service.GetByCode("ABC");
                Assert.NotNull(found);
                Assert.Equal(25, found.DiscountPercentage);
            }
        }


    }
}