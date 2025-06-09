using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.ServiceDtos;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Services.Queries
{
    public record GetServicesByProviderIdQuery(long ProviderId) : IRequest<IEnumerable<ServiceDto?>>;
    public class GetServicesByProviderIdQueryHandler(IServiceRepository serviceRepository, IMapper mapper) : IRequestHandler<GetServicesByProviderIdQuery, IEnumerable<ServiceDto?>>
    {
        private readonly IServiceRepository _serviceRepo = serviceRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<ServiceDto?>> Handle(GetServicesByProviderIdQuery request, CancellationToken cancellationToken)
        {
            long providerId = request.ProviderId;
            if (providerId <= 0)
            {
                throw new ArgumentException("Provider ID must be greater than zero.", nameof(request));
            }
            {
                var services = await _serviceRepo.GetServicesByProviderAsync(providerId);
                return _mapper.Map<IEnumerable<ServiceDto>>(services);
            }
        }
    }
}
