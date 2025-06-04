using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.AppontmentDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Appointments.Commands
{
    public record CreateAppointmentCommand(AppointmentCreateDto Dto) : IRequest<long>;

    public class CreateAppointmentHandler(IRepository<Appointment> appointmentRepo, IMapper mapper) : IRequestHandler<CreateAppointmentCommand, long>
    {
        private readonly IRepository<Appointment> _appointmentRepo = appointmentRepo;
        private readonly IMapper _mapper = mapper;

        public async Task<long> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = _mapper.Map<Appointment>(request.Dto);
            appointment.CreatedAt = DateTime.UtcNow;
            appointment.UpdatedAt = DateTime.UtcNow;

            return await _appointmentRepo.AddAsync(appointment);
        }
    }
}
