using System.ComponentModel.DataAnnotations;
using LabProject.Enums;

namespace LabProject.Models
{
    public class Provider
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ProviderSpecialty Specialty { get; set; } = ProviderSpecialty.General;
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
