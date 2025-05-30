using System.Threading.Tasks;
using LabProject.Application.Interfaces;
using LabProject.Application.Services;
using LabProject.Domain.Entities;
using LabProject.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController(IBaseService<Appointment> appointmentService) : ControllerBase
    {
        private readonly IBaseService<Appointment> _appointmentService = appointmentService;

        /// <summary>
        /// Gets all appointments in the system.
        /// </summary>
        /// <returns>A list of all appointments.</returns>
        /// <response code="200">Returns the list of all appointments.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return Ok(await _appointmentService.GetAllAsync());
        }

        /// <summary>
        /// Gets a specific appointment by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment.</param>
        /// <returns>The requested appointment.</returns>
        /// <response code="200">Returns the appointment with the given ID.</response>
        /// <response code="404">No appointment found with the specified ID.</response>

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointmentById([FromRoute] int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointment">The appointment object to create.</param>
        /// <returns>The created appointment.</returns>
        /// <response code="201">The appointment was created successfully.</response>
        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null)
                return BadRequest();

            var newId = await _appointmentService.AddAsync(appointment);
            if (newId <= 0)
            {
                return StatusCode(500, "Failed to create appointment");
            }

            var createdAppointment = await _appointmentService.GetByIdAsync(newId);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = newId }, createdAppointment);
        }


        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to update.</param>
        /// <param name="updatedAppointment">The updated appointment data.</param>
        /// <returns>The updated appointment.</returns>
        /// <response code="200">The appointment was updated successfully.</response>
        /// <response code="404">No appointment found with the specified ID.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAppointment([FromRoute] int id, [FromBody] Appointment updatedAppointment)
        {
            if (updatedAppointment == null || id != updatedAppointment.Id)
            {
                return BadRequest("Appointment data is invalid or ID mismatch.");
            }

            var success = await _appointmentService.UpdateAsync(id, updatedAppointment);
            if (!success)
            {
                return NotFound($"Appointment with ID {id} not found.");
            }

            // Optionally, return the updated appointment data after update
            var appointment = await _appointmentService.GetByIdAsync(id);
            return Ok(appointment);
        }

        /// <summary>
        /// Deletes an appointment by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to delete.</param>
        /// <returns>Status of the deletion.</returns>
        /// <response code="200">The appointment was deleted successfully.</response>
        /// <response code="404">No appointment found with the specified ID.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAppointment([FromRoute] int id)
        {
            var success = await _appointmentService.DeleteAsync(id);
            if (!success)
            {
                return NotFound($"Appointment with ID {id} not found.");
            }
            return Ok();
        }
    }
}
