using Calendar.Data;
using Calendar.Models;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly CalendarDbContext _context;

        public CalendarService(CalendarDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConsultantsCalendars>> GetAllAsync()
        {
            return await _context.Calendars.ToListAsync();
        }

        public async Task<ConsultantsCalendars> GetByIdAsync(int id)
        {
            return await _context.Calendars.FindAsync(id);
        }

        public async Task<int> CreateAsync(ConsultantsCalendars consultant)
        {
            _context.Calendars.Add(consultant);

            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ConsultantsCalendars consultant)
        {
            _context.Entry(consultant).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var consultant = await _context.Calendars.FindAsync(id);
            _context.Calendars.Remove(consultant);

            return await _context.SaveChangesAsync();
        }
    }
}
