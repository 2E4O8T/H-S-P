using System.ComponentModel.DataAnnotations;

namespace Calendar.Models
{
    public class ConsultantsCalendars
    {
        public int Id { get; set; }
        public string Patient { get; set; } = string.Empty;
        public string Consultant {  get; set; } = string.Empty;
        [DataType(DataType.Date)] 
        public DateTime AppointmentDate { get; set; }= DateTime.Now;
    }
}
