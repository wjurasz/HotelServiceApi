using HotelService.Promotion.Storage.Context;
using HotelService.Promotion.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelService.PromotionApi.Services
{
    public class PromotionService
    {
        private readonly PromotionDbContext _context;

        public PromotionService(PromotionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promotion.Storage.Entities.Promotion>> GetAll()
        {
            return await _context.Promotions
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Promotion.Storage.Entities.Promotion?> GetById(int id)
        {
            return await _context.Promotions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Promotion.Storage.Entities.Promotion?> GetByCode(string code)
        {
            return await _context.Promotions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Code == code &&
                    (p.ValidUntil == null || p.ValidUntil > DateTime.Now));
        }

        public async Task Add(Promotion.Storage.Entities.Promotion promotion)
        {
            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var promo = await _context.Promotions.FindAsync(id);
            if (promo != null)
            {
                _context.Promotions.Remove(promo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
