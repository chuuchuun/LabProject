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

namespace LabProject.Application.Features.Locations.Queries
{
    public record GetAllLocationsQuery : IRequest<IEnumerable<LocationDto>>;
    public class GetAllLocationsQueryHandler(IRepository<Location> repository, IMapper mapper) 
        : IRequestHandler<GetAllLocationsQuery, IEnumerable<LocationDto>>
    {
        public async Task<IEnumerable<LocationDto>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<LocationDto>>(locations);
        }
    }
}
