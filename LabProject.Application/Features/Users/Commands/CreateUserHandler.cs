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
            var passwordHash = !string.IsNullOrEmpty(request.Dto.Password) ?
                BCrypt.Net.BCrypt.HashPassword(request.Dto.Password) : null;
            if (passwordHash is not null)
            {
                var user = _mapper.Map<User>(request.Dto);
                user.PasswordHash = passwordHash;
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;
                return await _userRepo.AddAsync(user);
            }
            throw new ArgumentException("Password cannot be null or empty");
        }
    }
}
