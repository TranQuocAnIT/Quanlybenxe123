using Microsoft.EntityFrameworkCore;
using Quanlybenxe123.Data;
using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public class EFBusTripRepository : IBusTripRepository
    {
        private readonly ApplicationDbContext _context;

        public EFBusTripRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BusTrip> GetByIdAsync(int id)
        {
            return await _context.BusTrips.FindAsync(id);
        }

        public async Task<List<BusTrip>> GetAllAsync()
        {
            return await _context.BusTrips.ToListAsync();
        }

        public async Task AddAsync(BusTrip busTrip)
        {
            _context.BusTrips.Add(busTrip);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BusTrip busTrip)
        {
            _context.Entry(busTrip).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bustrip = await _context.BusTrips.FindAsync(id);
            _context.BusTrips.Remove(bustrip);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveImages(IEnumerable<BusTripImage> images)
        {
            _context.BusTripImages.RemoveRange(images);
            await _context.SaveChangesAsync();
        }
    }
}
