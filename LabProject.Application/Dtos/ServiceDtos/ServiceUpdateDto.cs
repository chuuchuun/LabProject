using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.ServiceDtos
{
    public record ServiceUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? DurationMinutes { get; set; }
        public decimal? Price { get; set; }
    }
}
