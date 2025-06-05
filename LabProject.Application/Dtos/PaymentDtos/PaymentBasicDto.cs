using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.PaymentDtos
{
    public record PaymentBasicDto
    {
        public long Id { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
