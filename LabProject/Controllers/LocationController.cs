using LabProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Controllers
{
    /// <summary>
    /// Controller for managing locations where services are provided.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private static List<Location> Locations = new List<Location>
        {
            new Location {Id = 1, Name = "Best Beauty", Address = "Main street 12", City = "Gdansk", Phone = "123456789"},
            new Location {Id = 2, Name = "New you", Address = "City square", City = "Warsaw", Phone = "987654321"},
        };

        /// <summary>
        /// Retrieves all locations.
        /// </summary>
        /// <returns>A list of all locations.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetLocations()
        {
            return Ok(Locations);
        }

        /// <summary>
        /// Retrieves a specific location by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the location.</param>
        /// <returns>The location matching the ID or 404 if not found.</returns>
        [HttpGet("{id}")]
        public ActionResult<Location> GetLocationById(int id)
        {
            var location = Locations.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="location">The location data to create.</param>
        /// <returns>The newly created location with its assigned ID.</returns>
        [HttpPost]
        public ActionResult<Location> CreateLocation(Location location)
        {
            location.Id = Locations.Any() ? Locations.Max(l => l.Id) + 1 : 1;
            Locations.Add(location);
            return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
        }

        /// <summary>
        /// Updates an existing location.
        /// </summary>
        /// <param name="id">The ID of the location to update.</param>
        /// <param name="location">The updated location data.</param>
        /// <returns>The updated location or 404 if the location does not exist.</returns>
        [HttpPut("{id}")]
        public ActionResult UpdateLocation(int id, Location location)
        {
            var locationToUpdate = Locations.FirstOrDefault(l => l.Id == id);
            if (locationToUpdate == null)
            {
                return NotFound();
            }
            locationToUpdate.Name = location.Name;
            locationToUpdate.Address = location.Address;
            locationToUpdate.City = location.City;
            locationToUpdate.Phone = location.Phone;
            return Ok(locationToUpdate);
        }

        /// <summary>
        /// Deletes a location by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the location to delete.</param>
        /// <returns>No content if deletion is successful, or 404 if location not found.</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteLocation(int id)
        {
            var location = Locations.FirstOrDefault(l => l.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            Locations.Remove(location);
            return NoContent();
        }
    }
}
