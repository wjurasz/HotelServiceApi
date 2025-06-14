using HotelService.Reservation.Storage.Context;
using HotelService.Reservation.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelService.ReservationApi.Services
{
    public class ReservationService
    {
        private readonly ReservationDbContext _context;

        public ReservationService(ReservationDbContext context)
        {
            _context = context;
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
    }
}
