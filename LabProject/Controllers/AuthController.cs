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

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserCreateDto userDto)
        {
            var validRoles = new[] { "Admin", "Client", "Provider" };
            if (!validRoles.Contains(userDto.RoleName))
            {
                return BadRequest("Invalid role specified");
            }

            var result = await _mediator.Send(new CreateUserCommand(
                new UserCreateDto
                {
                    Name = userDto.Name,
                    Username = userDto.Username,
                    Phone = userDto.Phone,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    RoleName = userDto.RoleName
                }));

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var token = await _mediator.Send(new LoginUserCommand(loginDto.Username, loginDto.Password));
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
