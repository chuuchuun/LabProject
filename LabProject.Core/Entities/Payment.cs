using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public long Id { get; set; }
        public long AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = null!;
        public decimal AmountPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public ICollection<PaymentDiscount> PaymentsDiscounts { get; set; } = [];
    }
}
