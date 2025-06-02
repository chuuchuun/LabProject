using System.ComponentModel.DataAnnotations;

namespace LabProject.Domain.Entities
{
    public class Review : BaseEntity
    {
        public long Id { get; set; }

        [Required]
        public long ClientId { get; set; }

        [Required]
        public long AppointmentId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

        public User? Client { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
