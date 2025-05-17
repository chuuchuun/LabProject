using System.ComponentModel.DataAnnotations;
using LabProject.Enums;

namespace LabProject.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        //public Client Client { get; set; }
        //public Provider Provider { get; set; }
        //public Service Service { get; set; }
        //public Location Location { get; set; }
    }
}
