using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [EmailAddress]
        public required string EmailAddress { get; set; }
        [Phone]
        public required string Phone { get; set; }
    }
}
