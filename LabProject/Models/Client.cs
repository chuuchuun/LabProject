using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Phone]
        public string Phone { get; set; }
        //public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        //public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
