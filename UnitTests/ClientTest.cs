using HotelService.ClientApi;
using HotelService.ClientApi.Entities;
using HotelService.ClientApi.Services;
using HotelService.Reservation.Storage.Context;
using HotelService.ReservationApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ClientServiceTests
    {
        private ClientService GetServiceWithInMemoryDb(out ClientDbContext context)
        {
            var options = new DbContextOptionsBuilder<ClientDbContext>()
                .UseInMemoryDatabase(databaseName: "ClientDbTest")
                .Options;

            context = new ClientDbContext(null!);
            return new ClientService(context);
        }

        [Fact]
        public async Task Add_ShouldAddClient()
        {
            var options = new DbContextOptionsBuilder<ClientDbContext>()
                .UseInMemoryDatabase("AddClientDb")
                .Options;

            using var context = new ClientDbContext(null!);
            var service = new ClientService(context);
            var client = new Client
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "jan@test.com",
                PhoneNumber = "123456789"
            };

            await service.Add(client);

            var added = context.Clients.FirstOrDefault(c => c.Email == "jan@test.com");
            Assert.NotNull(added);
        }

        [Fact]
        public async Task GetById_ShouldReturnClient()
        {
            var options = new DbContextOptionsBuilder<ClientDbContext>()
                .UseInMemoryDatabase("GetByIdDb")
                .Options;

            using var context = new ClientDbContext(null!);
            var service = new ClientService(context);
            var client = new Client
            {
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna@test.com",
                PhoneNumber = "987654321"
            };
            context.Clients.Add(client);
            context.SaveChanges();

            var result = await service.GetById(client.Id);
            Assert.NotNull(result);
            Assert.Equal("Anna", result.FirstName);
        }

        [Fact]
        public async Task Delete_ShouldRemoveClient()
        {
            var options = new DbContextOptionsBuilder<ClientDbContext>()
                .UseInMemoryDatabase("DeleteClientDb")
                .Options;

            using var context = new ClientDbContext(null!);
            var service = new ClientService(context);
            var client = new Client
            {
                FirstName = "Kamil",
                LastName = "Nowak",
                Email = "kamil@test.com",
                PhoneNumber = "987654321"
            };
            context.Clients.Add(client);
            context.SaveChanges();

            await service.Delete(client.Id);

            var deleted = context.Clients.Find(client.Id);
            Assert.Null(deleted);
        }
    }
}