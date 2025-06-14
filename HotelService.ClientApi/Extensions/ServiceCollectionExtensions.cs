using HotelService.ClientApi.Services;

namespace HotelService.ClientApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services)
        {
            services.AddDbContext<ClientDbContext, ClientDbContext>();
            services.AddTransient<ClientService>();
            return services;
        }

    }
}
