using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public long Id { get; set; }
        [Required]
        public long ClientId { get; set; }
        [Required]
        [MaxLength(100)]
        required public string Title { get; set; }
        public decimal Value { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        public DateTime? ValidUntil { get; set;}
        public User? Client { get; set; }
        public ICollection<PaymentDiscount> PaymentsDiscounts { get; set; } = [];
    }
}
