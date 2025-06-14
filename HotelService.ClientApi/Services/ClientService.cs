using HotelService.ClientApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelService.ClientApi.Services
{
    public class ClientService
    {
        private readonly ClientDbContext _context;

        public ClientService(ClientDbContext context)
        {
            _context = context;
        }

        public async Task<Client> GetById(int id)
        {
            return await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Client>> Get()
        {
            return await _context.Clients
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task Add(Client entity)
        {
            _context.Clients.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }


    }
}
