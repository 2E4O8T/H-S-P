using Calendar.Models;

namespace Calendar.Services
{
    public interface ICalendarService
    {
        Task<IEnumerable<ConsultantsCalendars>> GetAllAsync();
        Task<ConsultantsCalendars> GetByIdAsync(int id);
        Task<int> CreateAsync(ConsultantsCalendars consultant);
        Task UpdateAsync(ConsultantsCalendars consultant);
        Task<int> DeleteAsync(int id);
    }
}
