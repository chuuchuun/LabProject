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
    public record CreateLocationCommand(LocationCreateDto Dto) : IRequest<long>;
    public class CreateLocationHandler(IRepository<Location> locationRepo, IMapper mapper): IRequestHandler<CreateLocationCommand, long>
    {
        private readonly IRepository<Location> _locationRepo = locationRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<long> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = _mapper.Map<Location>(request.Dto);
            return await _locationRepo.AddAsync(location);
        }
    }
}
