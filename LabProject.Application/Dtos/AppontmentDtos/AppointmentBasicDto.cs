using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.AppontmentDtos
{
    public class AppointmentBasicDto
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        required public string Status { get; set; }
    }
}
