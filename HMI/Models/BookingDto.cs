using System.ComponentModel.DataAnnotations;

namespace HMI.Models
{
    public class BookingDto
    {
        public string Patient { get; set; }
        public string Consultant { get; set; }
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
    }
}
