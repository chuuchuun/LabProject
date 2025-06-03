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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetClients()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users.Where(u => u.RoleId == 3));
        }

        /// <summary>
        /// Gets a specific client by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the client.</param>
        /// <returns>The client matching the ID.</returns>
        /// <response code="200">Returns the client with the specified ID.</response>
        /// <response code="404">No client found with the specified ID.</response>
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

            // Optionally, return the updated appointment data after update
            var provider = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(provider);
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
    }
}
