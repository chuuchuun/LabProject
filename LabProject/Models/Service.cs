using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1, 180)]
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
    }
}
