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
    public record GetLocationByIdQuery (long Id) : IRequest<LocationDto?>;
    public class GetLocationByIdQueryHandler(IRepository<Location> locationRepository, IMapper mapper) 
        : IRequestHandler<GetLocationByIdQuery, LocationDto?>
    {
        public async Task<LocationDto?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await locationRepository.GetByIdAsync(request.Id);
            return location is null ? null : mapper.Map<LocationDto>(location);
        }
    }
}
