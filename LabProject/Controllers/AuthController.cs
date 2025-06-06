using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Features.Users.Commands;
using LabProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using LabProject.Application.Interfaces;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserCreateDto userDto)
        {
            var validRoles = new[] { "Admin", "Client", "Provider" };
            if (!validRoles.Contains(userDto.RoleName))
            {
                return BadRequest("Invalid role specified");
            }

            var result = await _userService.AddAsync(
                new UserCreateDto
                {
                    Name = userDto.Name,
                    Username = userDto.Username,
                    Phone = userDto.Phone,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    RoleName = userDto.RoleName
                });

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var token = await _userService.LoginUser(loginDto);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
