using Booking.Models;

namespace Booking.Services
{
    public interface IBookingService
    {
        public Task<int> AddBooking(BookingDto bookingDto);
        public Task <IEnumerable<BookingDto>> GetAllBookings();
    }
}
