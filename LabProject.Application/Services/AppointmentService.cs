using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class AppointmentService(IRepository<Appointment> appointmentRepo) : IBaseService<Appointment>
    {
        private readonly IRepository<Appointment> _appointmentRepo = appointmentRepo;

        public async Task<long> AddAsync(Appointment entityModel)
        {
            return await _appointmentRepo.AddAsync(entityModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _appointmentRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _appointmentRepo.GetAllAsync();
        }

        public async Task<Appointment?> GetByIdAsync(long id)
        {
            return await _appointmentRepo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(long id, Appointment entityModel)
        {
            return await _appointmentRepo.UpdateAsync(id, entityModel);
        }
    }
}
