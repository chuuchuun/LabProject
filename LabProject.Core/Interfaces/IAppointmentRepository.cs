using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;

namespace LabProject.Domain.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByClientIdAsync(long clientId);
        Task<IEnumerable<Appointment>> GetUpcomingAppointmentsForProviderAsync(long providerId);
    }
}
