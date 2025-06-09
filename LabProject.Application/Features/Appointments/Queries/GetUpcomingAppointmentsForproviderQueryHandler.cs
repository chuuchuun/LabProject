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
    public record GetUpcomingAppointmentsForProviderQuery(long ProviderId) : IRequest<IEnumerable<AppointmentDto?>>;
    public class GetUpcomingAppointmentsForproviderQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        : IRequestHandler<GetUpcomingAppointmentsForProviderQuery, IEnumerable<AppointmentDto?>>
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<AppointmentDto?>> Handle(GetUpcomingAppointmentsForProviderQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetUpcomingAppointmentsForProviderAsync(request.ProviderId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
    }
}
