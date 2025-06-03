using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.LocationDtos
{
    public class LocationDto : BaseDto
    {
        public long Id { get; set; }
        required public string Name { get; set; }
        required public string Address { get; set; }
        required public string City { get; set; }
        required public string Phone { get; set; }
    }
}
