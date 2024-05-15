using Quanlybenxe123.Models;

namespace Quanlybenxe123.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> GetByIdAsync(int id);
        Task<List<Booking>> GetAllAsync();
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
    }
}
