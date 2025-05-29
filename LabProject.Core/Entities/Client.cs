using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Domain.Entities
{
    public class Client
    {
        public required User User { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<Review>? Reviews  { get; set; }
        public ICollection<Discount>? Discounts { get; set; }
        public ICollection<User>? FavouriteProviders { get; set; }
    }
}
