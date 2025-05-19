using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        [Phone]
        public string Phone { get; set; }

    }
}
