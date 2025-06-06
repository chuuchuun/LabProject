using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.RoleDtos;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;

namespace LabProject.Application.Services
{
    public class RoleService(IRoleRepository roleRepo, IMapper mapper) : IBaseService<Role, RoleDto, RoleCreateDto, RoleUpdateDto>
    {
        private readonly IRoleRepository _roleRepo = roleRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<long> AddAsync(RoleCreateDto roleCreateDto)
        {
            var roleEntity = _mapper.Map<Role>(roleCreateDto);
            roleEntity.CreatedAt = DateTime.UtcNow;
            roleEntity.UpdatedAt = DateTime.UtcNow;

            return await _roleRepo.AddAsync(roleEntity);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _roleRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _roleRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto?> GetByIdAsync(long id)
        {
            var role = await _roleRepo.GetByIdAsync(id);
            return role is null ? null : _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> UpdateAsync(long id, RoleUpdateDto roleUpdateDto)
        {
            var existingRole = await _roleRepo.GetByIdAsync(id);
            if (existingRole is null)
                return false;

            _mapper.Map(roleUpdateDto, existingRole);
            existingRole.UpdatedAt = DateTime.UtcNow;

            return await _roleRepo.UpdateAsync(id, existingRole);
        }
    }
}
