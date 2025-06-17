using HotelService.Promotion.Storage.Context;
using HotelService.Promotion.Storage.Entities;
using HotelService.PromotionApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class PromotionServiceTests
    {
        private PromotionService GetServiceWithInMemoryDb(out PromotionDbContext context)
        {
            var options = new DbContextOptionsBuilder<PromotionDbContext>()
                .UseInMemoryDatabase(databaseName: "PromotionDbTest")
                .Options;

            context = new PromotionDbContext(null!);
            return new PromotionService(context);
        }

        [Fact]
        public async Task Add_ShouldAddPromotion()
        {
            var options = new DbContextOptionsBuilder<PromotionDbContext>()
                .UseInMemoryDatabase("AddPromotionDb")
                .Options;

            using var context = new PromotionDbContext(null!);
            var service = new PromotionService(context);
            var promotion = new Promotion
            {
                Code = "SUMMER2025",
                DiscountPercentage = 15,
                ValidUntil = DateTime.Now.AddMonths(1)
            };

            await service.Add(promotion);

            var added = context.Promotions.FirstOrDefault(p => p.Code == "SUMMER2025");
            Assert.NotNull(added);
        }

        [Fact]
        public async Task GetById_ShouldReturnPromotion()
        {
            var options = new DbContextOptionsBuilder<PromotionDbContext>()
                .UseInMemoryDatabase("GetByIdPromotionDb")
                .Options;

            using var context = new PromotionDbContext(null!);
            var service = new PromotionService(context);
            var promotion = new Promotion
            {
                Code = "WINTER2025",
                DiscountPercentage = 20,
                ValidUntil = DateTime.Now.AddMonths(1)
            };
            context.Promotions.Add(promotion);
            context.SaveChanges();

            var result = await service.GetById(promotion.Id);
            Assert.NotNull(result);
            Assert.Equal("WINTER2025", result.Code);
        }
        [Fact]
        public async Task Delete_ShouldRemovePromotion()
        {
            var options = new DbContextOptionsBuilder<PromotionDbContext>()
                .UseInMemoryDatabase("DeletePromotionDb")
                .Options;

            using var context = new PromotionDbContext(null!);
            var service = new PromotionService(context);
            var promotion = new Promotion
            {
                Code = "AUTUMN2025",
                DiscountPercentage = 10,
                ValidUntil = DateTime.Now.AddMonths(3)
            };
            context.Promotions.Add(promotion);
            context.SaveChanges();

            await service.Delete(promotion.Id);

            var deleted = context.Promotions.Find(promotion.Id);
            Assert.Null(deleted);
        }
    }
}
