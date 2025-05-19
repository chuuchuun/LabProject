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
        private static List<Provider> Providers = new List<Provider>
        {
            new Provider { Id = 1, Name = "Alice Smith", Email = "alice@example.com", Phone = "123456789", Specialty = ProviderSpecialty.HairStylist },
            new Provider { Id = 2, Name = "John Doe", Email = "john@example.com", Phone = "987654321", Specialty = ProviderSpecialty.Dentist }
        };

        /// <summary>
        /// Gets all providers in the system.
        /// </summary>
        /// <returns>
        /// 200 OK – A list of all providers.
        /// </returns>
        [HttpGet]
        public ActionResult<IEnumerable<Provider>> GetProviders()
        {
            return Ok(Providers);
        }

        /// <summary>
        /// Gets a specific provider by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the provider.</param>
        /// <returns>
        /// 200 OK – The provider matching the ID.<br/>
        /// 404 Not Found – No provider found with the given ID.
        /// </returns>
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
        /// <returns>
        /// 201 Created – The created provider with its assigned ID.
        /// </returns>
        [HttpPost]
        public ActionResult<Provider> CreateProvider([FromBody] Provider provider)
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
        /// <returns>
        /// 200 OK – The updated provider.<br/>
        /// 404 Not Found – No provider found with the given ID.
        /// </returns>
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
        /// <returns>
        /// 200 OK – Deletion successful.<br/>
        /// 404 Not Found – No provider found with the given ID.
        /// </returns>
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
