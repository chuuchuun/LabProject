using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.ServiceDtos;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Services.Commands
{
    public record UpdateServiceCommand(long Id, ServiceUpdateDto Dto) : IRequest<bool>;
    public class UpdateServiceHandler(IMapper mapper, IServiceRepository serviceRepository)
                : IRequestHandler<UpdateServiceCommand, bool>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        public async Task<bool> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var existingService = await _serviceRepository.GetByIdAsync(request.Id);
            if (existingService is null)
                return false;
            _mapper.Map(request.Dto, existingService);
            existingService.UpdatedAt = DateTime.UtcNow;
            return await _serviceRepository.UpdateAsync(request.Id, existingService);
        }
    }
}
