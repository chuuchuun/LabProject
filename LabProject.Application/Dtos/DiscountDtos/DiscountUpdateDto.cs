using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.DiscountDtos
{

    public record DiscountUpdateDto
    {
        [MaxLength(100)]
        public string? Title { get; set; }

        public decimal? Value { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
