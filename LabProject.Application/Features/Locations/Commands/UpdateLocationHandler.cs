using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using MediatR;

namespace LabProject.Application.Features.Locations.Commands
{
    public record UpdateLocationCommand(long Id, LocationUpdateDto Dto) : IRequest<bool>;

    public class UpdateLocationHandler(IRepository<Location> repository, IMapper mapper) : IRequestHandler<UpdateLocationCommand, bool>
    {
        private readonly IRepository<Location> _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<bool> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _repository.GetByIdAsync(request.Id);
            if (location is null) return false;
    
            _mapper.Map(request.Dto, location);
            location.UpdatedAt = DateTime.UtcNow;

            return await _repository.UpdateAsync(request.Id, location);
        }
    }
}
