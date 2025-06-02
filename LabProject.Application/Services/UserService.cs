using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class UserService(IUserRepository userRepo) : IUserService
    {
        private readonly IUserRepository _userRepo = userRepo;

        public async Task<long> AddAsync(User entityModel)
        {
            return await _userRepo.AddAsync(entityModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _userRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(long id, User entityModel)
        {
            return await _userRepo.UpdateAsync(id, entityModel);
        }

        public async Task<IEnumerable<User>> GetProvidersBySpecialtyIdAsync(long specialtyId)
        {
            return await _userRepo.GetProvidersBySpecialtyAsync(specialtyId);
        }
    }
}
