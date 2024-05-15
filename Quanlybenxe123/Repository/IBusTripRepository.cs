using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public interface IBusTripRepository
    {
        Task<BusTrip> GetByIdAsync(int id);
        Task<List<BusTrip>> GetAllAsync();
        Task AddAsync(BusTrip busTrip);
        Task UpdateAsync(BusTrip busTrip);
        Task DeleteAsync(int id);
        Task RemoveImages(IEnumerable<BusTripImage> images);
    }
}
