using System.ComponentModel.DataAnnotations;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Domain.Entities
{
    public class Service : BaseEntity
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(100)]
        required public string Name { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        [Range(0,180)]
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public ICollection<User> Providers { get; set; } = [];
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}
