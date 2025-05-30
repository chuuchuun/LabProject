using System;
using System.ComponentModel.DataAnnotations;
using LabProject.Domain.Enums;

namespace LabProject.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public long Id { get; set; }

        [Required]
        public long ClientId { get; set; }

        [Required]
        public long ProviderId { get; set; }

        [Required]
        public long ServiceId { get; set; }

        [Required]
        public long LocationId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
        public User? Client { get; set; }
        public User? Provider { get; set; }
        public Service? Service { get; set; }
        public Location? Location { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = [];
    }
}
