using Booking.Models;

namespace Booking.Services
{
    public interface IConsultantService
    {
        Task<IEnumerable<Consultant>> GetAllAsync();
        Task<Consultant> GetByIdAsync(int id);
        Task<int> CreateAsync(Consultant consultant);
        Task UpdateAsync(Consultant consultant);
        Task<int> DeleteAsync(int id);
    }
}