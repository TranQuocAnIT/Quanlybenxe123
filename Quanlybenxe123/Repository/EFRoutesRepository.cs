using Microsoft.EntityFrameworkCore;
using Quanlybenxe123.Data;
using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public class EFRoutesRepository : IRoutesRepository
    {
        private readonly ApplicationDbContext _context;

        public EFRoutesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Routes> GetByIdAsync(int id)
        {
            return await _context.Routes.FindAsync(id);
        }

        public async Task<List<Routes>> GetAllAsync()
        {
            return await _context.Routes.ToListAsync();
        }

        public async Task AddAsync(Routes route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Routes route)
        {
            _context.Entry(route).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
        }
    }
}
