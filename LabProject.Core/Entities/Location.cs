using System.ComponentModel.DataAnnotations;

namespace LabProject.Domain.Entities
{
    public class Location : BaseEntity
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(100)]
        required public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        required public string Address { get; set; }
        [Required]
        [MaxLength(100)]
        required public string City { get; set; }
        [Required]
        [Phone]
        required public string Phone { get; set; }
        public ICollection<User> Providers { get; set; } = [];
        public ICollection<Appointment> Appointments { get; set; } = [];

    }
}
