using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class ServiceService(IServiceRepository serviceRepo) : IServiceService
    {
        private readonly IServiceRepository _serviceRepo = serviceRepo;

        public async Task<long> AddAsync(Service entityModel)
        {
            return await _serviceRepo.AddAsync(entityModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _serviceRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _serviceRepo.GetAllAsync();
        }

        public async Task<Service?> GetByIdAsync(long id)
        {
            return await _serviceRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Service>> GetServicesByProviderAsync(long providerId)
        {
            return await _serviceRepo.GetServicesByProviderAsync(providerId);
        }

        public async Task<bool> UpdateAsync(long id, Service entityModel)
        {
            return await _serviceRepo.UpdateAsync(id, entityModel);
        }
    }
}
