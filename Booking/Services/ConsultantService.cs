using Booking.Data;
using Booking.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Services
{
    public class ConsultantService : IConsultantService
    {
        private readonly BookingDbContext _context;

    public ConsultantService(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consultant>> GetAllAsync()
        {
            return await _context.Consultants.ToListAsync();
        }

        public async Task<Consultant> GetByIdAsync(int id)
        {
            return await _context.Consultants.FindAsync(id);
        }

        public async Task<int> CreateAsync(Consultant consultant)
        {
            _context.Consultants.Add(consultant);

            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Consultant consultant)
        {
            _context.Entry(consultant).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var consultant = await _context.Consultants.FindAsync(id);
            _context.Consultants.Remove(consultant);

            return await _context.SaveChangesAsync();
        }
    }
}
