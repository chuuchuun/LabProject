using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using MediatR;
using BCrypt.Net;

namespace LabProject.Application.Features.Users.Commands
{
    public record CreateUserCommand(UserCreateDto Dto) : IRequest<long>;

    public class CreateUserHandler(IUserRepository userRepo, IMapper mapper, IRoleRepository roleRepo) : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IUserRepository _userRepo = userRepo;
        private readonly IRoleRepository _roleRepo = roleRepo;
        private readonly IMapper _mapper = mapper;

        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string? passwordHash;
            if (!string.IsNullOrEmpty(request.Dto.Password))
            {
                passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);
            }
            else
            {
                throw new ArgumentException("Password cannot be null or empty");
            }
            var role = await _roleRepo.GetRoleByNameAsync(request.Dto.RoleName);
            var user = _mapper.Map<User>(request.Dto);
            user.PasswordHash = passwordHash;
            user.Role = role;
            user.RoleId = role?.Id ?? throw new ArgumentException("Role not found");
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            return await _userRepo.AddAsync(user);
        }
    }
}
