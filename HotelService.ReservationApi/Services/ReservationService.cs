using HotelService.Reservation.Storage.Context;
using HotelService.Reservation.Storage.Entities;
using HotelService.ReservationApi.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace HotelService.ReservationApi.Services
{
    public class ReservationService
    {
        private readonly ReservationDbContext _context;
        private readonly PromotionResolver _promotionResolver;

        public ReservationService(ReservationDbContext context, PromotionResolver promotionResolver)
        {
            _context = context;
            _promotionResolver = promotionResolver;
        }

        public async Task<IEnumerable<Reservation.Storage.Entities.Reservation>> GetAll()
        {
            return await _context.Reservations
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Reservation.Storage.Entities.Reservation?> GetById(int id)
        {
            return await _context.Reservations
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task Add(Reservation.Storage.Entities.Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Reservation.Storage.Entities.Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task Confirm(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.Status = ReservationStatus.Confirmed;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Cancel(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.Status = ReservationStatus.Cancelled;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> CalculateDiscountedPrice(DateTime start, DateTime end, int? promotionId)
        {
            var days = (end - start).Days;
            if (days <= 0) return 0;

            decimal basePrice = days * 100m;

            if (!promotionId.HasValue)
                return basePrice;

            var promotion = await _promotionResolver.GetPromotionById(promotionId.Value);
            if (promotion == null)
                return basePrice;

            return basePrice * (1 - (promotion.DiscountPercentage / 100m));
        }
    }
}
