using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.DiscountDtos;

namespace LabProject.Application.Dtos.PaymentDiscountDtos
{
    public class PaymentDiscountDto : BaseDto
    {
        public long DiscountId { get; set; }
        public long PaymentId { get; set; }
        public decimal? PayedValue { get; set; }
        required public DiscountBasicDto Discount { get; set; }
    }
}
