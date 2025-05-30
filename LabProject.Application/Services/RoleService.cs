using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class RoleService(IRepository<Role> roleRepo) : IBaseService<Role>
    {
        private readonly IRepository<Role> _roleRepo = roleRepo;

        public async Task<long> AddAsync(Role entityModel)
        {
            return await _roleRepo.AddAsync(entityModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _roleRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _roleRepo.GetAllAsync();
        }

        public async Task<Role?> GetByIdAsync(long id)
        {
            return await _roleRepo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(long id, Role entityModel)
        {
            return await _roleRepo.UpdateAsync(id, entityModel);
        }
    }
}
