using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Application.Dtos.ServiceDtos;

namespace LabProject.Application.Dtos.UserDtos
{
    public record UserProviderDto : UserBasicDto
    {
        public List<ServiceBasicDto> Services { get; set; } = [];
        public List<LocationBasicDto> Locations { get; set; } = [];
    }
}
