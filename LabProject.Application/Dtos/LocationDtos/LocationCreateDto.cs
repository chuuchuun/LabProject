using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.LocationDtos
{
    public class LocationCreateDto
    {
        [Required, MaxLength(100)]
        required public string Name { get; set; }

        [Required, MaxLength(255)]
        required public string Address { get; set; }

        [Required, MaxLength(100)]
        required public string City { get; set; }

        [Required, Phone]
        required public string Phone { get; set; }
    }
}
