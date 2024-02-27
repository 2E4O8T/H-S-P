using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    public class BookingDto
    {
        public string Patient {  get; set; } = string.Empty;
        public string Consultant { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
    }
}
