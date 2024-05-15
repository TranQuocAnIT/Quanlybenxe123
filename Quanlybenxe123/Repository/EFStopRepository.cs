using Microsoft.EntityFrameworkCore;
using Quanlybenxe123.Data;
using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public class EFStopRepository : IStopRepository
    {
        private readonly ApplicationDbContext _context;

        public EFStopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stop> GetByIdAsync(int id)
        {
            return await _context.Stops.FindAsync(id);
        }

        public async Task<List<Stop>> GetAllAsync()
        {
            return await _context.Stops.ToListAsync();
        }

        public async Task AddAsync(Stop stop)
        {
            _context.Stops.Add(stop);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stop stop)
        {
            _context.Entry(stop).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        { 
            var stop = await _context.Stops.FindAsync(id);
            _context.Stops.Remove(stop);
            await _context.SaveChangesAsync();
        }
    }
}
