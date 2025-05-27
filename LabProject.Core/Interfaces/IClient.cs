using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;

namespace LabProject.Domain.Interfaces
{
    public interface IClient : IUser
    {
        ICollection<User> FavouriteProviders { get; }
        ICollection<Appointment> AppointmentsAsClient { get; }
        ICollection<Review> Reviews { get; }
        ICollection<Discount> Discounts { get; }
    }
}
