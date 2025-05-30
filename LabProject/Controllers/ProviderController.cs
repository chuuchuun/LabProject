using LabProject.Domain.Enums;
using LabProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private static readonly List<User> Providers =
        [
            new User { Id = 1, Name = "Alice Smith", PasswordHash =" sdifojaiflj", Email = "alice@example.com", Phone = "123456789",  Username = "alice" },
            new User { Id = 2, Name = "John Doe", PasswordHash = " sgdgsd", Email = "john@example.com", Phone = "987654321", Username = "john" }   ];

        /// <summary>
        /// Gets all providers in the system.
        /// </summary>
        /// <returns>A list of all providers.</returns>
        /// <response code="200">Returns the list of providers.</response>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetProviders()
        {
            return Ok(Providers);
        }

        /// <summary>
        /// Gets a specific provider by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the provider.</param>
        /// <returns>The provider matching the ID.</returns>
        /// <response code="200">Returns the provider with the specified ID.</response>
        /// <response code="404">No provider found with the specified ID.</response>
        [HttpGet("{id}")]
        public ActionResult<User> GetProviderById([FromRoute] int id)
        {
            var provider = Providers.FirstOrDefault(p => p.Id == id);
            if (provider is null)
            {
                return NotFound();
            }
            return Ok(provider);
        }

        /// <summary>
        /// Creates a new provider.
        /// </summary>
        /// <param name="provider">The provider data to create.</param>
        /// <returns>The created provider with its assigned ID.</returns>
        /// <response code="201">The provider was created successfully.</response>
        [HttpPost]
        public ActionResult<User> CreateProvider([FromBody] User provider)
        {
            provider.Id = Providers.Count != 0 ? Providers.Max(p => p.Id) + 1 : 1;
            Providers.Add(provider);
            return CreatedAtAction(nameof(GetProviderById), new { id = provider.Id }, provider);
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
        public ActionResult UpdateProvider([FromRoute] int id, [FromBody] User updatedProvider)
        {
            var provider = Providers.FirstOrDefault(p => p.Id == id);
            if (provider is null)
                return NotFound();

            provider.Name = updatedProvider.Name;
            provider.Email =  updatedProvider.Email;
            provider.Phone = updatedProvider.Phone;
            provider.ProviderSpecialties = updatedProvider.ProviderSpecialties;

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
        public ActionResult DeleteProvider([FromRoute] int id)
        {
            var provider = Providers.FirstOrDefault(p => p.Id == id);
            if (provider is null)
            {
                return NotFound();
            }

            Providers.Remove(provider);
            return Ok();
        }
    }
}
