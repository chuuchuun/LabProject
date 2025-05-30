using LabProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private static readonly List<User> Clients = 
        [
            new User {Id = 1, Name = "Mary Sue", PasswordHash = "sfojfsfq3", Email = "mary@gmail.com", Phone = "123456789", Username ="mary"},
            new User { Id = 2, Name = "Jane Doe", PasswordHash = "addads", Email = "jane@gmail.com", Phone = "987654321", Username = "jane" }
        ];

        /// <summary>
        /// Gets all clients in the system.
        /// </summary>
        /// <returns>A list of all clients.</returns>
        /// <response code="200">Returns the list of all clients.</response>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetClients()
        {
            return Ok(Clients);
        }

        /// <summary>
        /// Gets a specific client by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the client.</param>
        /// <returns>The requested client.</returns>
        /// <response code="200">Returns the client with the given ID.</response>
        /// <response code="404">No client found with the specified ID.</response>
        [HttpGet("{id}")]
        public ActionResult<User> GetClientById([FromRoute] int id)
        {
            var client = Clients.FirstOrDefault(c => c.Id == id);
            if (client is null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="client">The client data to create.</param>
        /// <returns>The created client.</returns>
        /// <response code="201">The client was created successfully.</response>
        [HttpPost]
        public ActionResult<User> CreateClient([FromBody] User client)
        {
            client.Id = Clients.Count != 0 ? Clients.Max(c => c.Id) + 1 : 1;
            Clients.Add(client);
            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        /// <summary>
        /// Updates an existing client.
        /// </summary>
        /// <param name="id">The ID of the client to update.</param>
        /// <param name="client">The updated client data.</param>
        /// <returns>The updated client.</returns>
        /// <response code="200">The client was updated successfully.</response>
        /// <response code="404">No client found with the specified ID.</response>
        [HttpPut("{id}")]
        public ActionResult UpdateClient([FromRoute] int id, [FromBody] User client)
        {
            var clientToUpdate = Clients.FirstOrDefault(c => c.Id == id);
            if (clientToUpdate is null)
            {
                return NotFound();
            }
            clientToUpdate.Name = client.Name;
            clientToUpdate.Email = client.Email;
            clientToUpdate.Phone = client.Phone;
            return Ok(clientToUpdate);
        }

        /// <summary>
        /// Deletes a client by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the client to delete.</param>
        /// <returns>Status of the deletion.</returns>
        /// <response code="200">The client was deleted successfully.</response>
        /// <response code="404">No client found with the specified ID.</response>
        [HttpDelete("{id}")]
        public ActionResult DeleteClient([FromRoute] int id)
        {
            var client = Clients.FirstOrDefault(c => c.Id == id);
            if (client is null)
            {
                return NotFound();
            }
            Clients.Remove(client);
            return Ok();
        }
    }
}
