using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.DiscountDtos
{
    public class DiscountCreateDto
    {
        [Required]
        public long ClientId { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public decimal Value { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
