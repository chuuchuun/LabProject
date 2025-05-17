using LabProject.Enums;
using LabProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private static List<Provider> Providers = new List<Provider>
        {
            new Provider { Id = 1, Name = "Alice Smith", Email = "alice@example.com", Phone = "123456789", Specialty = ProviderSpecialty.HairStylist },
            new Provider { Id = 2, Name = "John Doe", Email = "john@example.com", Phone = "987654321", Specialty = ProviderSpecialty.Dentist }
        };

        /// <summary>
        /// Gets all providers in the system.
        /// </summary>
        /// <returns>A list of providers.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Provider>> GetProviders()
        {
            return Ok(Providers);
        }

        /// <summary>
        /// Gets a specific provider by ID.
        /// </summary>
        /// <param name="id">Provider's unique identifier.</param>
        /// <returns>The requested provider, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public ActionResult<Provider> GetProviderById(int id)
        {
            var provider = Providers.FirstOrDefault(p => p.Id == id);
            if (provider == null)
            {
                return NotFound();
            }
            return Ok(provider);
        }

        /// <summary>
        /// Creates a new provider.
        /// </summary>
        /// <param name="provider">The provider to create.</param>
        /// <returns>The created provider with a generated ID.</returns>
        [HttpPost]
        public ActionResult<Provider> CreateProvider(Provider provider)
        {
            provider.Id = Providers.Any() ? Providers.Max(p => p.Id) + 1 : 1;
            Providers.Add(provider);
            return CreatedAtAction(nameof(GetProviderById), new { id = provider.Id }, provider);
        }

        /// <summary>
        /// Updates an existing provider.
        /// </summary>
        /// <param name="id">The ID of the provider to update.</param>
        /// <param name="updatedProvider">The updated provider data.</param>
        /// <returns>The updated provider object, or 404 if not found.</returns>
        [HttpPut("{id}")]
        public ActionResult UpdateProvider(int id, Provider updatedProvider)
        {
            var provider = Providers.FirstOrDefault(p => p.Id == id);
            if (provider == null)
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
        /// <param name="id">Provider's unique identifier.</param>
        /// <returns>No content if deletion is successful, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteProvider(int id)
        {
            var provider = Providers.FirstOrDefault(p => p.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            Providers.Remove(provider);
            return NoContent();
        }
    }
}
