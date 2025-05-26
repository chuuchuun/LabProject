using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public required string Address { get; set; }
        public required string City { get; set; }
        [Phone]
        public required string Phone { get; set; }

    }
}
