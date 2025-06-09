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
    public record GetAllServicesQuery() : IRequest<IEnumerable<ServiceDto?>>;
    public class GetAllServicesQueryHandler(IServiceRepository serviceRepository, IMapper mapper) : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceDto?>>
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<ServiceDto?>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }
    }
}
