using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class LocationService(IRepository<Location> locationRepo) : IBaseService<Location>
    {
        private readonly IRepository<Location> _locationRepo = locationRepo;

        public async Task<long> AddAsync(Location entityModel)
        {
            return await _locationRepo.AddAsync(entityModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _locationRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _locationRepo.GetAllAsync();
        }

        public async Task<Location?> GetByIdAsync(long id)
        {
            return await _locationRepo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(long id, Location entityModel)
        {
            return await _locationRepo.UpdateAsync(id, entityModel);
        }
    } 
}
