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
    public class AppointmentDto : BaseDto
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ProviderId { get; set; }
        public long ServiceId { get; set; }
        public long LocationId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; } = "Scheduled";

        // Navigation properties as DTOs
        public UserBasicDto? Client { get; set; }
        public UserBasicDto? Provider { get; set; }
        public ServiceBasicDto? Service { get; set; }
        public LocationBasicDto? Location { get; set; }
    }


}
