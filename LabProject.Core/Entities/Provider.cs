using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Enums;

namespace LabProject.Domain.Entities
{
    public class Provider
    {
        public required User User { get; set; }
        public ICollection<Appointment>? AppointmentsAsProvider { get; set; }
        public ICollection<User>? FavouritesAsProvider { get; set; }
        required public ICollection<Service> ProviderServices { get; set; }
        required public ICollection<Location> ProviderLocations { get; set; }
        public ICollection<ProviderSpecialty>? ProviderSpecialties { get; set; }
    }
}
