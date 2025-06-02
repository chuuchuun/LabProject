using LabProject.Domain.Enums;
using LabProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using LabProject.Application.Interfaces;
using System.Threading.Tasks;
using LabProject.Application.Services;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController(IUserService userService): ControllerBase
    {
        private readonly IUserService _userService = userService;
        /// <summary>
        /// Gets all providers in the system.
        /// </summary>
        /// <returns>A list of all providers.</returns>
        /// <response code="200">Returns the list of providers.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetProviders()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users.Where(u => u.RoleId == 2));
        }

        /// <summary>
        /// Gets a specific provider by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the provider.</param>
        /// <returns>The provider matching the ID.</returns>
        /// <response code="200">Returns the provider with the specified ID.</response>
        /// <response code="404">No provider found with the specified ID.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetProviderById([FromRoute] int id)
        {
            var provider = await _userService.GetByIdAsync(id);
            if (provider is null)
                return NotFound();

            return Ok(provider);
        }

        [HttpGet("/specialties/{specialtyId}")]
        public async Task<ActionResult<IEnumerable<User>>> GetProvidersBySpecialtyId([FromRoute] long specialtyId)
        {
            return Ok(await _userService.GetProvidersBySpecialtyIdAsync(specialtyId));
        }
        /// <summary>
        /// Creates a new provider.
        /// </summary>
        /// <param name="provider">The provider data to create.</param>
        /// <returns>The created provider with its assigned ID.</returns>
        /// <response code="201">The provider was created successfully.</response>
        [HttpPost]
        public async Task<ActionResult<User>> CreateProvider([FromBody] User provider)
        {
            if (provider is null)
                return BadRequest();

            var newId = await _userService.AddAsync(provider);
            if (newId <= 0)
            {
                return StatusCode(500, "Failed to create provider");
            }

            var createdAppointment = await _userService.GetByIdAsync(newId);
            return CreatedAtAction(nameof(GetProviderById), new { id = newId}, createdAppointment);
        }
        /// <summary>
        /// Updates an existing provider.
        /// </summary>
        /// <param name="id">The ID of the provider to update.</param>
        /// <param name="updatedProvider">The updated provider data.</param>
        /// <returns>The updated provider.</returns>
        /// <response code="200">The provider was updated successfully.</response>
        /// <response code="404">No provider found with the specified ID.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProvider([FromRoute] int id, [FromBody] User updatedProvider)
        {
            if (updatedProvider is null || id != updatedProvider.Id)
            {
                return BadRequest("Provider data is invalid or ID mismatch.");
            }

            var success = await _userService.UpdateAsync(id, updatedProvider);
            if (!success)
            {
                return NotFound($"Provider with ID {id} not found.");
            }

            // Optionally, return the updated appointment data after update
            var provider = await _userService.GetByIdAsync(id);
            return Ok(provider);
        }

        /// <summary>
        /// Deletes a provider by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the provider to delete.</param>
        /// <returns>Status of the deletion.</returns>
        /// <response code="200">The provider was deleted successfully.</response>
        /// <response code="404">No provider found with the specified ID.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProvider([FromRoute] int id)
        {
            var success = await _userService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Provider with ID {id} not found.");
            }
            return Ok();
        }
    }
}
