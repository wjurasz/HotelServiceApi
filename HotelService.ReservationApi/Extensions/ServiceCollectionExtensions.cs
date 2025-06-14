using HotelService.Reservation.Storage.Context;
using HotelService.ReservationApi.Services;

namespace HotelService.ReservationApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddReservationServices(this IServiceCollection services)
        {
            services.AddDbContext<ReservationDbContext>();
            services.AddTransient<ReservationService>();
            return services;
        }

    }
}
