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
    public class AuthController(IUserService userService, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly ILogger<AuthController> _logger = logger;
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserCreateDto userDto)
        {
            _logger.LogInformation("Registering new user: {Username}", userDto.Username);
            try
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
                _logger.LogInformation("User registered successfully: {Username} with Id {Id}", userDto.Username, result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user: {Username}", userDto.Username);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                _logger.LogInformation("User login attempt: {Username}", loginDto.Username);
                var token = await _userService.LoginUser(loginDto);
                _logger.LogInformation("User logged in successfully: {Username}", loginDto.Username);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogWarning("Invalid login attempt for user: {Username}", loginDto.Username);
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
