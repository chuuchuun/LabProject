using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Dtos.AppontmentDtos;
using LabProject.Application.Dtos.PaymentDiscountDtos;

namespace LabProject.Application.Dtos.PaymentDtos
{
    public class PaymentDto : BaseDto
    {
        public long Id { get; set; }
        public long AppointmentId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public required AppointmentBasicDto Appointment { get; set; }
        public List<PaymentDiscountDto> AppliedDiscounts { get; set; } = [];
    }
}
