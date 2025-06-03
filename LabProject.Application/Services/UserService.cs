using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BCrypt.Net;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class UserService(IUserRepository userRepo, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepo = userRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<long> AddAsync(UserCreateDto userCreateDto)
        {
            var userEntity = _mapper.Map<User>(userCreateDto);
            if (!string.IsNullOrEmpty(userCreateDto.Password))
            {
                userEntity.PasswordHash = userCreateDto.Password;
            }
            else
            {
                throw new ArgumentException("Password cannot be null or empty");
            }
            userEntity.CreatedAt = DateTime.UtcNow;
            userEntity.UpdatedAt = DateTime.UtcNow;

            return await _userRepo.AddAsync(userEntity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _userRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(long id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }


        public async Task<bool> UpdateAsync(long id, UserUpdateDto userUpdateDto)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);
            if (existingUser is null)
                return false;

            _mapper.Map(userUpdateDto, existingUser);
            existingUser.UpdatedAt = DateTime.UtcNow;

            return await _userRepo.UpdateAsync(id, existingUser);
        }

        public async Task<IEnumerable<UserProviderDto>> GetProvidersBySpecialtyIdAsync(long specialtyId)
        {
            var providers = await _userRepo.GetProvidersBySpecialtyAsync(specialtyId);
            return _mapper.Map<IEnumerable<UserProviderDto>>(providers);
        }
    }
}
