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

namespace LabProject.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserHandler(IUserRepository userRepo, IMapper mapper) : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IUserRepository _userRepo = userRepo;
        private readonly IMapper _mapper = mapper;

        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.Dto);
            if (!string.IsNullOrEmpty(request.Dto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);
            }
            else
            {
                throw new ArgumentException("Password cannot be null or empty");
            }
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            return await _userRepo.AddAsync(user);
        }
    }
}
