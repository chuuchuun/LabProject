using LabProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private static List<Service> Services = new List<Service>
        {
            new Service { Id = 1, Name = "Haircut", Description = "Basic haircut service", DurationMinutes = 30, Price = 25.00m },
            new Service { Id = 2, Name = "Teeth Cleaning", Description = "Dental cleaning procedure", DurationMinutes = 45, Price = 75.00m }
        };

        /// <summary>
        /// Gets all services in the system.
        /// </summary>
        /// <returns>A list of services.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            return Ok(Services);
        }

        /// <summary>
        /// Gets a specific service by ID.
        /// </summary>
        /// <param name="id">Service's unique identifier.</param>
        /// <returns>The requested service, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public ActionResult<Service> GetServiceById(int id)
        {
            var service = Services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        /// <summary>
        /// Creates a new service.
        /// </summary>
        /// <param name="service">The service to create.</param>
        /// <returns>The created service with a generated ID.</returns>
        [HttpPost]
        public ActionResult<Service> CreateService(Service service)
        {
            service.Id = Services.Any() ? Services.Max(s => s.Id) + 1 : 1;
            Services.Add(service);
            return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, service);
        }

        /// <summary>
        /// Updates an existing service.
        /// </summary>
        /// <param name="id">The ID of the service to update.</param>
        /// <param name="updatedService">The updated service data.</param>
        /// <returns>The updated service object, or 404 if not found.</returns>
        [HttpPut("{id}")]
        public ActionResult UpdateService(int id, Service updatedService)
        {
            var service = Services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            service.Name = updatedService.Name;
            service.Description = updatedService.Description;
            service.DurationMinutes = updatedService.DurationMinutes;
            service.Price = updatedService.Price;

            return Ok(service);
        }

        /// <summary>
        /// Deletes a service by ID.
        /// </summary>
        /// <param name="id">Service's unique identifier.</param>
        /// <returns>No content if deletion is successful, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteService(int id)
        {
            var service = Services.FirstOrDefault(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            Services.Remove(service);
            return NoContent();
        }
    }
}
