using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.LocationDtos
{
    public record LocationCreateDto
    {
        [Required, MaxLength(100)]
        required public string Name { get; set; }

        [Required, MaxLength(255)]
        required public string Address { get; set; }

        [Required, MaxLength(100)]
        required public string City { get; set; }

        required public string Phone { get; set; }
    }
}
