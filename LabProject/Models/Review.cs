using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int ProviderId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

    }
}
