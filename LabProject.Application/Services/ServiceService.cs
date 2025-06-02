using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.ServiceDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class ServiceService(IServiceRepository serviceRepo, IMapper mapper) : IServiceService
    {
        private readonly IServiceRepository _serviceRepo = serviceRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<long> AddAsync(ServiceCreateDto serviceCreateDto)
        {
            var serviceEntity = _mapper.Map<Service>(serviceCreateDto);
            serviceEntity.CreatedAt = DateTime.UtcNow;
            serviceEntity.UpdatedAt = DateTime.UtcNow;

            return await _serviceRepo.AddAsync(serviceEntity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _serviceRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync()
        {
            var services = await _serviceRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<ServiceDto?> GetByIdAsync(long id)
        {
            var service = await _serviceRepo.GetByIdAsync(id);
            return service is null ? null : _mapper.Map<ServiceDto>(service);

        }

        public async Task<IEnumerable<ServiceDto>> GetServicesByProviderAsync(long providerId)
        {
            var services = await _serviceRepo.GetServicesByProviderAsync(providerId);
            return _mapper.Map<IEnumerable<ServiceDto>>(services);
        }

        public async Task<bool> UpdateAsync(long id, ServiceUpdateDto serviceUpdateDto)
        {
            var existingService = await _serviceRepo.GetByIdAsync(id);
            if (existingService is null)
                return false;

            _mapper.Map(serviceUpdateDto, existingService);
            existingService.UpdatedAt = DateTime.UtcNow;

            return await _serviceRepo.UpdateAsync(id, existingService);
        }
    }
}
