using LabProject.Domain.Enums;
using LabProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using LabProject.Application.Interfaces;
using System.Threading.Tasks;
using LabProject.Application.Services;
using LabProject.Application.Dtos.UserDtos;
using MediatR;
using LabProject.Application.Features.Users.Commands;
using LabProject.Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using LabProject.Application.Features.Appointments.Queries;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        /// <summary>
        /// Gets all clients in the system.
        /// </summary>
        /// <returns>A list of all clients.</returns>
        /// <response code="200">Returns the list of clients.</response>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetClients()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users.Where(u => u.RoleName == "Client"));
        }

        /// <summary>
        /// Gets currently logged in client.
        /// </summary>
        /// <returns>The currently logged in client.</returns>
        /// <response code="200">Returns the currently logged in client.</response>
        /// <response code="401">No client ID was found in access token.</response>
        /// <response code="403">No corresponding client found with the specified ID.</response>
        [Authorize(Roles = "Client")]
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetMe()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var user = await _mediator.Send(new GetUserByIdQuery(userId));
            if (user == null || user.RoleName != "Client")
            {
                return Forbid("Access denied. Only clients can access this endpoint.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Gets a specific client by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the client.</param>
        /// <returns>The client matching the ID.</returns>
        /// <response code="200">Returns the client with the specified ID.</response>
        /// <response code="404">No client found with the specified ID.</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetClientById([FromRoute] int id)
        {
            var client = await _mediator.Send(new GetUserByIdQuery(id));
            if (client is null)
                return NotFound();

            return Ok(client);
        }

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="userCreateDto">The client data to create.</param>
        /// <returns>The created client with its assigned ID.</returns>
        /// <response code="201">The client was created successfully.</response>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateClient([FromBody] UserCreateDto userCreateDto)
        {
            if (userCreateDto is null)
                return BadRequest();

            var newId = await _mediator.Send(new CreateUserCommand(userCreateDto));
            if (newId <= 0)
            {
                return StatusCode(500, "Failed to create client");
            }

            var createdClient = await _mediator.Send(new GetUserByIdQuery(newId));
            return CreatedAtAction(nameof(GetClientById), new { id = newId }, createdClient);
        }
        /// <summary>
        /// Updates an existing client.
        /// </summary>
        /// <param name="id">The ID of the client to update.</param>
        /// <param name="updatedProvider">The updated client data.</param>
        /// <returns>The updated client.</returns>
        /// <response code="200">The client was updated successfully.</response>
        /// <response code="404">No client found with the specified ID.</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient([FromRoute] int id, [FromBody] UserUpdateDto updatedProvider)
        {
            if (updatedProvider is null)
            {
                return BadRequest("Client data is invalid or ID mismatch.");
            }

            var success = await _mediator.Send(new UpdateUserCommand(id, updatedProvider));
            if (!success)
            {
                return NotFound($"Client with ID {id} not found.");
            }
            var provider = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(provider);
        }

        [Authorize(Roles = "Client")]
        [HttpPut("me")]
        public async Task<ActionResult<UserDto>> UpdateMe([FromBody] UserUpdateDto userUpdateDto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }
            await _mediator.Send(new UpdateUserCommand(userId, userUpdateDto));
            var updated = await _mediator.Send(new GetUserByIdQuery(userId));

            return Ok(updated);
        }
        /// <summary>
        /// Deletes a client by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the client to delete.</param>
        /// <returns>Status of the deletion.</returns>
        /// <response code="200">The client was deleted successfully.</response>
        /// <response code="404">No client found with the specified ID.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient([FromRoute] int id)
        {
            var success = await _mediator.Send(new DeleteUserCommand(id));
            if (!success)
            {
                return NotFound($"Client with ID {id} not found.");
            }
            return Ok();
        }

        [HttpGet("appointments/{id}")]
        public async Task<ActionResult> GetAppointmentsForClient([FromRoute] long id)
        {
            var appointments = await _mediator.Send(new GetAppointmentsForUserQuery(id));
            if (appointments is null || !appointments.Any())
            {
                return NotFound($"No appointments were found for client with ID {id}.");
            }
            return Ok(appointments);
        }
    }
}
