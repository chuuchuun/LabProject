using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Application.Dtos.PaymentDtos
{
    public record PaymentCreateDto
    {
        [Required]
        public long AppointmentId { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        public List<long>? DiscountIds { get; set; }
    }
}
