using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.AppontmentDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class AppointmentService(IRepository<Appointment> appointmentRepo, IMapper mapper) : IBaseService<Appointment, AppointmentDto, AppointmentCreateDto, AppointmentUpdateDto>
    {
        private readonly IRepository<Appointment> _appointmentRepo = appointmentRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<long> AddAsync(AppointmentCreateDto appointmentCreateDto)
        {
            var appointmentEntity = _mapper.Map<Appointment>(appointmentCreateDto);
            appointmentEntity.CreatedAt = DateTime.UtcNow;
            appointmentEntity.UpdatedAt = DateTime.UtcNow;
            return await _appointmentRepo.AddAsync(appointmentEntity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _appointmentRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _appointmentRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto?> GetByIdAsync(long id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            return appointment is null ? null : _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<bool> UpdateAsync(long id, AppointmentUpdateDto appointmentUpdateDto)
        {
            var existingAppointment = await _appointmentRepo.GetByIdAsync(id);
            if (existingAppointment is null)
                return false;

            _mapper.Map(appointmentUpdateDto, existingAppointment);
            existingAppointment.UpdatedAt = DateTime.UtcNow;

            return await _appointmentRepo.UpdateAsync(id, existingAppointment);
        }
    }
}
