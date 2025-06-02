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
        public string Name { get; set; }

        [Required, MaxLength(255)]
        public string Address { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }

        [Required, Phone]
        public string Phone { get; set; }
    }
}
