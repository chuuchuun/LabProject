using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.ServiceDtos;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Services.Commands
{
    public record CreateServiceCommand(ServiceCreateDto Dto) : IRequest<long>;
    public class CreateServiceHandler(IServiceRepository serviceRepository, IMapper mapper) 
        : IRequestHandler<CreateServiceCommand, long>
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<long> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = _mapper.Map<Service>(request.Dto);
            service.CreatedAt = DateTime.UtcNow;
            service.UpdatedAt = DateTime.UtcNow;
            var createdService = await _serviceRepository.AddAsync(service);
            return createdService;
        }
    }
}
