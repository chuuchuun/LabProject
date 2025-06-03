using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Application.Dtos.ServiceDtos;
using LabProject.Application.Dtos.UserDtos;

namespace LabProject.Application.Dtos.AppontmentDtos
{
    public class AppointmentClientViewDto
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        required public string Status { get; set; }
        required public UserBasicDto Provider { get; set; }
        required public ServiceBasicDto Service { get; set; }
        required public LocationBasicDto Location { get; set; }
    }
}
