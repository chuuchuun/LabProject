using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.AppontmentDtos;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Appointments.Queries
{
    public record GetAppointmentsForUserQuery(long Id) : IRequest<IEnumerable<AppointmentDto?>>;

    public class GetAppointmentsForUserQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        : IRequestHandler<GetAppointmentsForUserQuery, IEnumerable<AppointmentDto?>>
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<AppointmentDto?>> Handle(GetAppointmentsForUserQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByClientIdAsync(request.Id);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
    }
}
