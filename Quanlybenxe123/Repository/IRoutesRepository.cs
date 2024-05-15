using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public interface IRoutesRepository
    {
        Task<Routes> GetByIdAsync(int id);
        Task<List<Routes>> GetAllAsync();
        Task AddAsync(Routes route);
        Task UpdateAsync(Routes route);
        Task DeleteAsync(int id);
    }
}
