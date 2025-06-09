using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BCrypt.Net;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Interfaces;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LabProject.Application.Services
{
    public class UserService(IUserRepository userRepo, IRoleRepository roleRepo, IMapper mapper, IConfiguration config) : IUserService
    {
        private readonly IUserRepository _userRepo = userRepo;
        private readonly IRoleRepository _roleRepo = roleRepo;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _config = config;   
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

        public async Task<string> LoginUser(UserLoginDto userLoginDto)
        {
            var user = await _userRepo.GetUserByUsernameAsync(userLoginDto.Username);

            if (user is null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var role = await _roleRepo.GetByIdAsync(user.RoleId) ?? throw new Exception("Role not found");
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role, role.Name)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
