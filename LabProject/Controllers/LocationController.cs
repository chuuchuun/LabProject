using System.Threading.Tasks;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Application.Features.Locations.Commands;
using LabProject.Application.Interfaces;
using LabProject.Application.Services;
using LabProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController(IBaseService<Location, LocationDto, LocationCreateDto, LocationUpdateDto> locationService, IMediator mediator) : ControllerBase
    {
        private readonly IBaseService<Location, LocationDto, LocationCreateDto, LocationUpdateDto> _locationService = locationService;
        private readonly IMediator _mediator = mediator;
        /// <summary>
        /// Gets all locations in the system.
        /// </summary>
        /// <returns>A list of all locations.</returns>
        /// <response code="200">Returns the list of all locations.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
        {
            return Ok(await _locationService.GetAllAsync());
        }

        /// <summary>
        /// Retrieves a specific location by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the location.</param>
        /// <returns>The location matching the ID.</returns>
        /// <response code="200">Returns the location with the specified ID.</response>
        /// <response code="404">No location found with the specified ID.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetLocationById([FromRoute] int id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location is null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="location">The location data to create.</param>
        /// <returns>The created location.</returns>
        /// <response code="201">The location was created successfully.</response>
        [HttpPost]
        public async Task<ActionResult<LocationDto>> CreateLocation([FromBody] LocationCreateDto location)
        {
            if(location is null)
            {
                return BadRequest();
            }
            var newId = await _mediator.Send(new CreateLocationCommand(location));
            if (newId < 0)
            {
                return StatusCode(500, "Failed to create location");

            }
            var createdLocation = await _locationService.GetByIdAsync(newId);
            return CreatedAtAction(nameof(GetLocationById), new { id = newId }, createdLocation);
        }

        /// <summary>
        /// Updates an existing location.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="location">The updated location data.</param>
        /// <returns>The updated location.</returns>
        /// <response code="200">The location was updated successfully.</response>
        /// <response code="404">No location found with the specified ID.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLocation([FromRoute] int id, [FromBody] LocationUpdateDto location)
        {
            if (location is null)
            {
                return BadRequest("Location data is invalid or ID mismatch.");
            }

            var success = await _locationService.UpdateAsync(id, location);
            if (!success)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            // Optionally, return the updated appointment data after update
            var updatedLocation = await _locationService.GetByIdAsync(id);
            return Ok(updatedLocation);
        }

        /// <summary>
        /// Deletes a location by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the location to delete.</param>
        /// <returns>Status of the deletion.</returns>
        /// <response code="200">The location was deleted successfully.</response>
        /// <response code="404">No location found with the specified ID.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLocation([FromRoute] int id)
        {
            var success = await _locationService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Location with ID {id} not found.");
            }
            return Ok();
        }
    }
}
