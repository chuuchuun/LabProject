using LabProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private static readonly List<Service> Services =
        [
            new Service { Id = 1, Name = "Haircut", Description = "Basic haircut service", DurationMinutes = 30, Price = 25.00m },
            new Service { Id = 2, Name = "Teeth Cleaning", Description = "Dental cleaning procedure", DurationMinutes = 45, Price = 75.00m }
        ];

        /// <summary>
        /// Gets all services in the system.
        /// </summary>
        /// <returns>List of all services.</returns>
        /// <response code="200">OK – A list of all services.</response>
        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetServices()
        {
            return Ok(Services);
        }

        /// <summary>
        /// Gets a specific service by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the service.</param>
        /// <returns>The service matching the ID.</returns>
        /// <response code="200">OK – The service matching the ID.</response>
        /// <response code="404">Not Found – No service found with the given ID.</response>
        [HttpGet("{id}")]
        public ActionResult<Service> GetServiceById([FromRoute] int id)
        {
            var service = Services.FirstOrDefault(s => s.Id == id);
            if (service is null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        /// <summary>
        /// Creates a new service.
        /// </summary>
        /// <param name="service">The service data to create.</param>
        /// <returns>The created service with assigned ID.</returns>
        /// <response code="201">Created – The created service with assigned ID.</response>
        [HttpPost]
        public ActionResult<Service> CreateService([FromBody] Service service)
        {
            service.Id = Services.Count != 0 ? Services.Max(s => s.Id) + 1 : 1;
            Services.Add(service);
            return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, service);
        }

        /// <summary>
        /// Updates an existing service.
        /// </summary>
        /// <param name="id">The ID of the service to update.</param>
        /// <param name="updatedService">The updated service data.</param>
        /// <returns>The updated service.</returns>
        /// <response code="200">OK – The updated service.</response>
        /// <response code="404">Not Found – No service found with the given ID.</response>
        [HttpPut("{id}")]
        public ActionResult UpdateService([FromRoute] int id, [FromBody] Service updatedService)
        {
            var service = Services.FirstOrDefault(s => s.Id == id);
            if (service is null)
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
        /// <param name="id">The unique identifier of the service to delete.</param>
        /// <returns>Status of the deletion.</returns>
        /// <response code="200">OK – Deletion successful.</response>
        /// <response code="404">Not Found – No service found with the given ID.</response>
        [HttpDelete("{id}")]
        public ActionResult DeleteService([FromRoute] int id)
        {
            var service = Services.FirstOrDefault(s => s.Id == id);
            if (service is null)
            {
                return NotFound();
            }
            Services.Remove(service);
            return Ok();
        }
    }
}
