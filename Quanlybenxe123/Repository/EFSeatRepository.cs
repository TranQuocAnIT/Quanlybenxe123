using Microsoft.EntityFrameworkCore;
using Quanlybenxe123.Data;
using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public class EFSeatRepository : ISeatRepository
    {
        private readonly ApplicationDbContext _context;

        public EFSeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Seat> GetByIdAsync(int id)
        {
            return await _context.Seats.FindAsync(id);
        }

        public async Task<List<Seat>> GetAllAsync()
        {
            return await _context.Seats.ToListAsync();
        }

        public async Task AddAsync(Seat seat)
        {
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seat seat)
        {
            _context.Entry(seat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();
        }
    }
}
