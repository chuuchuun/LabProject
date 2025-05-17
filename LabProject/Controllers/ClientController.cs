using LabProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private static List<Client> Clients = new List<Client>
        {
            new Client {Id = 1, Name = "Mary Sue", EmailAddress = "mary@gmail.com", Phone = "123456789"},
            new Client {Id = 2, Name = "Jane Doe", EmailAddress = "jane@gmail.com", Phone = "987654321"}
        };
        /// <summary>
        /// Gets all clients in the system.
        /// </summary>
        /// <returns>A list of clients.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            return Ok(Clients);
        }
        /// <summary>
        /// Gets a specific client by ID.
        /// </summary>
        /// <param name="id">Client's unique identifier.</param>
        /// <returns>The requested client, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public ActionResult<Client> GetClientById(int id)
        {
            var client = Clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="client">The client object to create.</param>
        /// <returns>The created client with generated ID.</returns>
        [HttpPost]
        public ActionResult<Client> CreateClient(Client client)
        {
            client.Id = Clients.Any()? Clients.Max(c => c.Id) + 1 : 1;
            Clients.Add(client);
            return CreatedAtAction(nameof(GetClientById), new {id = client.Id}, client);
        }
        /// <summary>
        /// Updates a specific client by ID.
        /// </summary>
        /// <param name="id">Client's unique identifier.</param>
        /// <param name="client">The client object to use for update.</param>
        /// <returns>The updated client object, or 404 if not found.</returns>
        [HttpPut("{id}")]
        public ActionResult UpdateClient(int id, Client client)
        {
            var clientToUpdate = Clients.FirstOrDefault(c=> c.Id == id);
            if( clientToUpdate == null)
            {
                return NotFound();
            }
            clientToUpdate.Name = client.Name;
            clientToUpdate.EmailAddress = client.EmailAddress;
            clientToUpdate.Phone = client.Phone;
            return Ok(clientToUpdate);
        }
        /// <summary>
        /// Deletes a client by ID.
        /// </summary>
        /// <param name="id">Client's unique identifier.</param>
        /// <returns>No content if deletion is successful, or 404 if not found.</returns>
        [HttpDelete]
        public ActionResult DeleteClient(int id) { 
            var client = Clients.FirstOrDefault(c=> c.Id == id);
            if (client == null) { 
                return NotFound();
            }
            Clients.Remove(client);
            return NoContent();
        }
    }
}
