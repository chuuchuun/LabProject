using LabProject.Enums;
using LabProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LabProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private static readonly List<Provider> Providers =
        [
            new Provider { Id = 1, Name = "Alice Smith", Email = "alice@example.com", Phone = "123456789", Specialty = ProviderSpecialty.HairStylist },
            new Provider { Id = 2, Name = "John Doe", Email = "john@example.com", Phone = "987654321", Specialty = ProviderSpecialty.Dentist }
        ];

        /// <summary>
        /// Gets all providers in the system.
        /// </summary>
        /// <returns>A list of all providers.</returns>
        /// <response code="200">Returns the list of providers.</response>
        [HttpGet]
        public ActionResult<IEnumerable<Provider>> GetProviders()
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
        public ActionResult<Provider> GetProviderById([FromRoute] int id)
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
        public ActionResult<Provider> CreateProvider([FromBody] Provider provider)
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
        public ActionResult UpdateProvider([FromRoute] int id, [FromBody] Provider updatedProvider)
        {
            var provider = Providers.FirstOrDefault(p => p.Id == id);
            if (provider is null)
            {
                return NotFound();
            }

            provider.Name = updatedProvider.Name;
            provider.Email = updatedProvider.Email;
            provider.Phone = updatedProvider.Phone;
            provider.Specialty = updatedProvider.Specialty;

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
