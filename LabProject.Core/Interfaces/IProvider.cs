using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Enums;
using LabProject.Domain.Entities;

namespace LabProject.Domain.Interfaces
{
    public interface IProvider : IUser
    {
        ICollection<User> FavouritedByClients { get; }
        ICollection<Appointment> AppointmentsAsProvider { get; }
        ICollection<ProviderSpecialty> ProviderSpecialties { get; }
        ICollection<Service> ProviderServices { get; }
        ICollection<Location> ProviderLocations { get; }
    }
}
