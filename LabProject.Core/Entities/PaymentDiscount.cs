using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Domain.Entities
{
    public class PaymentDiscount : BaseEntity
    {
        [Required]
        required public long DiscountId { get; set; }
        public Discount? Discount { get; set; }
        [Required]
        required public long PaymentId { get; set; }
        public Payment? Payment { get; set; }
        public decimal? PayedValue { get; set; }
    }
}
