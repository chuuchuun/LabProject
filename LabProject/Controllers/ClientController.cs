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
        /// <returns>
        /// 200 OK – A list of all clients.
        /// </returns>
        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            return Ok(Clients);
        }

        /// <summary>
        /// Gets a specific client by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the client.</param>
        /// <returns>
        /// 200 OK – The client matching the ID.<br/>
        /// 404 Not Found – No client found with the given ID.
        /// </returns>
        [HttpGet("{id}")]
        public ActionResult<Client> GetClientById([FromRoute] int id)
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
        /// <returns>
        /// 201 Created – The created client with its assigned ID.
        /// </returns>
        [HttpPost]
        public ActionResult<Client> CreateClient([FromBody] Client client)
        {
            client.Id = Clients.Any() ? Clients.Max(c => c.Id) + 1 : 1;
            Clients.Add(client);
            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
        }

        /// <summary>
        /// Updates an existing client.
        /// </summary>
        /// <param name="id">The ID of the client to update.</param>
        /// <param name="client">The updated client data.</param>
        /// <returns>
        /// 200 OK – The updated client.<br/>
        /// 404 Not Found – No client found with the given ID.
        /// </returns>
        [HttpPut("{id}")]
        public ActionResult UpdateClient([FromRoute] int id, [FromBody] Client client)
        {
            var clientToUpdate = Clients.FirstOrDefault(c => c.Id == id);
            if (clientToUpdate is null)
            {
                return NotFound();
            }
            clientToUpdate.Name = client.Name;
            clientToUpdate.EmailAddress = client.EmailAddress;
            clientToUpdate.Phone = client.Phone;
            return Ok(clientToUpdate);
        }

        /// <summary>
        /// Deletes a client by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the client to delete.</param>
        /// <returns>
        /// 200 OK – Deletion successful.<br/>
        /// 404 Not Found – No client found with the given ID.
        /// </returns>
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
