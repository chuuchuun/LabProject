using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.DiscountDtos
{
    public class DiscountBasicDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
    }
}
