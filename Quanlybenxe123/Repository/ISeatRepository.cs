using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public interface ISeatRepository
    {
        Task<Seat> GetByIdAsync(int id);
        Task<List<Seat>> GetAllAsync();
        Task AddAsync(Seat seat);
        Task UpdateAsync(Seat seat);
        Task DeleteAsync(int id);
    }
}
