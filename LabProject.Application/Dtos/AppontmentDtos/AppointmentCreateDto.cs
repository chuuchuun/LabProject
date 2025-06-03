using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.AppontmentDtos
{
    public class AppointmentCreateDto
    {
        [Required]
        public long ClientId { get; set; }

        [Required]
        public long ProviderId { get; set; }

        [Required]
        public long ServiceId { get; set; }

        [Required]
        public long LocationId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
