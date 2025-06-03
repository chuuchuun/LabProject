using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class LocationService(IRepository<Location> locationRepo, IMapper mapper) : IBaseService<Location, LocationDto, LocationCreateDto, LocationUpdateDto>
    {
        private readonly IRepository<Location> _locationRepo = locationRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<long> AddAsync(LocationCreateDto locationCreateDto)
        {
            var locationEntity = _mapper.Map<Location>(locationCreateDto);
            locationEntity.CreatedAt = DateTime.UtcNow;
            locationEntity.UpdatedAt = DateTime.UtcNow;

            return await _locationRepo.AddAsync(locationEntity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _locationRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<LocationDto>> GetAllAsync()
        {
            var locations = await _locationRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        public async Task<LocationDto?> GetByIdAsync(long id)
        {
            var location = await _locationRepo.GetByIdAsync(id);
            return location is null ? null : _mapper.Map<LocationDto>(location);
        }

        public async Task<bool> UpdateAsync(long id, LocationUpdateDto locationUpdateDto)
        {
            var existingLocation = await _locationRepo.GetByIdAsync(id);
            if (existingLocation is null)
                return false;

            _mapper.Map(locationUpdateDto, existingLocation);
            existingLocation.UpdatedAt = DateTime.UtcNow;

            return await _locationRepo.UpdateAsync(id, existingLocation);
        }
    } 
}
