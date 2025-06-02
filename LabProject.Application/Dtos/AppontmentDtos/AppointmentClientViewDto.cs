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
        public string Status { get; set; }
        public UserBasicDto Provider { get; set; }
        public ServiceBasicDto Service { get; set; }
        public LocationBasicDto Location { get; set; }
    }
}
