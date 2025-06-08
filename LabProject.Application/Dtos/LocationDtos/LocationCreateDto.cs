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
        required public string Name { get; set; }
        required public string Address { get; set; }
        required public string City { get; set; }
        required public string Phone { get; set; }
    }
}
