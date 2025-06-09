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
    public record GetServiceByIdQuery(long Id) : IRequest<ServiceDto?>;
    public class GetServiceByIdQueryHandler(IServiceRepository serviceRepository, IMapper mapper)
        : IRequestHandler<GetServiceByIdQuery, ServiceDto?>
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<ServiceDto?> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(request.Id);
            return service is null ? null : _mapper.Map<ServiceDto>(service);
        }
    }
}
