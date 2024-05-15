using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public interface IStopRepository
    {
        Task<Stop> GetByIdAsync(int id);
        Task<List<Stop>> GetAllAsync();
        Task AddAsync(Stop stop);
        Task UpdateAsync(Stop stop);
        Task DeleteAsync(int id);
    }
}
