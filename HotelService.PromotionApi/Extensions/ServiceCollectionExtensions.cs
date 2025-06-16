using HotelService.Promotion.Storage.Context;
using HotelService.PromotionApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelService.PromotionApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPromotionServices(this IServiceCollection services)
        {
            services.AddDbContext<PromotionDbContext>();
            services.AddTransient<PromotionService>();

            return services;
        }
    }
}
