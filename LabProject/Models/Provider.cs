using System.ComponentModel.DataAnnotations;
using LabProject.Enums;

namespace LabProject.Models
{
    public class Provider
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        public ProviderSpecialty Specialty { get; set; } = ProviderSpecialty.General;
        [EmailAddress]
        public required string Email { get; set; }
        [Phone]
        public required string Phone { get; set; }
    }
}
