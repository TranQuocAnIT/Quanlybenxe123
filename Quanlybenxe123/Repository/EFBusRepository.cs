using Microsoft.EntityFrameworkCore;
using Quanlybenxe123.Data;
using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public class EFBusRepository : IBusRepository
    {
        private readonly ApplicationDbContext _context;

        public EFBusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bus> GetByIdAsync(int id)
        {
            return await _context.Buses.FindAsync(id);
        }

        public async Task<List<Bus>> GetAllAsync()
        {
            return await _context.Buses.ToListAsync();
        }

        public async Task AddAsync(Bus bus)
        {
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Bus bus)
        {
            _context.Entry(bus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bus = await _context.Buses.FindAsync(id);
            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync();
        }
    }
}
