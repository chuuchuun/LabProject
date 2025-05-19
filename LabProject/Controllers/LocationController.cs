using LabProject.Models;
using Microsoft.AspNetCore.Mvc;

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
    /// Gets all locations in the system.
    /// </summary>
    /// <returns>
    /// 200 OK – A list of all locations.
    /// </returns>
    [HttpGet]
    public ActionResult<IEnumerable<Location>> GetLocations()
    {
        return Ok(Locations);
    }

    /// <summary>
    /// Retrieves a specific location by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the location.</param>
    /// <returns>
    /// 200 OK – The location matching the ID.<br/>
    /// 404 Not Found – No location found with the given ID.
    /// </returns>
    [HttpGet("{id}")]
    public ActionResult<Location> GetLocationById([FromRoute] int id)
    {
        var location = Locations.FirstOrDefault(l => l.Id == id);
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
    /// <returns>
    /// 201 Created – The  created location with its assigned ID.
    /// </returns>
    [HttpPost]
    public ActionResult<Location> CreateLocation([FromBody] Location location)
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
    /// <returns>
    /// 200 OK – The updated location.<br/>
    /// 404 Not Found – No location found with the given ID.
    /// </returns>
    [HttpPut("{id}")]
    public ActionResult UpdateLocation([FromRoute] int id,[FromBody] Location location)
    {
        var locationToUpdate = Locations.FirstOrDefault(l => l.Id == id);
        if (locationToUpdate is null)
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
    /// <returns>
    /// 200 Ok – Deletion successful.<br/>
    /// 404 Not Found – No location found with the given ID.
    /// </returns>
    [HttpDelete("{id}")]
    public ActionResult DeleteLocation([FromRoute] int id)
    {
        var location = Locations.FirstOrDefault(l => l.Id == id);
        if (location is null)
        {
            return NotFound();
        }
        Locations.Remove(location);
        return Ok();
    }
}
