using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.AppontmentDtos
{
    public class AppointmentUpdateDto
    {
        public DateTime? DateTime { get; set; }
        public long? LocationId { get; set; }
        public string? Status { get; set; }
    }
}
