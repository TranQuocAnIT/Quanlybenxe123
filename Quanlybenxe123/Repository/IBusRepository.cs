using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public interface IBusRepository
    {
        Task<Bus> GetByIdAsync(int id);
        Task<List<Bus>> GetAllAsync();
        Task AddAsync(Bus bus);
        Task UpdateAsync(Bus bus);
        Task DeleteAsync(int id);
    }
}
