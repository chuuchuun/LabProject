using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.ServiceDtos
{
    public class ServiceUpdateDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(0, 180)]
        public int? DurationMinutes { get; set; }

        public decimal? Price { get; set; }
    }
}
