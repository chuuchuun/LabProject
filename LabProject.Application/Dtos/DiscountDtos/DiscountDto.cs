using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.UserDtos;

namespace LabProject.Application.Dtos.DiscountDtos
{
    public class DiscountDto : BaseDto
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        required public string Title { get; set; }
        public decimal Value { get; set; }
        public string? Description { get; set; }
        public DateTime? ValidUntil { get; set; }
        public UserBasicDto? Client { get; set; }
    }
}
