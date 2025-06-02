using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.AppontmentDtos;
using LabProject.Application.Dtos.UserDtos;

namespace LabProject.Application.Dtos.ReviewDtos
{
    public class ReviewDto : BaseDto
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long AppointmentId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime DatePosted { get; set; }
        public UserBasicDto Client { get; set; }
        public AppointmentBasicDto Appointment { get; set; }
    }

}
