using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LabProject.Application.Features.Users.Commands
{
    public record LoginUserCommand(string Username, string Password) : IRequest<string>;

    public class LoginUserCommandHandler(IUserRepository userRepo, IRoleRepository roleRepo, IConfiguration config) : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepo = userRepo;
        private readonly IConfiguration _config = config;
        private readonly IRoleRepository _roleRepo = roleRepo;

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetUserByUsernameAsync(request.Username);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
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
