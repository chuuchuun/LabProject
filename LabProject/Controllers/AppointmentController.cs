using LabProject.Domain.Entities;
using LabProject.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private static readonly List<Appointment> Appointments =
            [
            new Appointment { Id = 1, ClientId = 1, ProviderId = 1, ServiceId = 1, LocationId = 1, DateTime = DateTime.Today, Status = AppointmentStatus.Completed },
            new Appointment { Id = 2, ClientId = 2, ProviderId = 1, ServiceId = 2, LocationId = 1, DateTime = DateTime.Today.AddDays(1) }
            ];

        /// <summary>
        /// Gets all appointments in the system.
        /// </summary>
        /// <returns>A list of all appointments.</returns>
        /// <response code="200">Returns the list of all appointments.</response>
        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return Ok(Appointments);
        }

        /// <summary>
        /// Gets a specific appointment by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment.</param>
        /// <returns>The requested appointment.</returns>
        /// <response code="200">Returns the appointment with the given ID.</response>
        /// <response code="404">No appointment found with the specified ID.</response>
        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointmentById([FromRoute] int id)
        {
            var appointment = Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment is null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointment">The appointment object to create.</param>
        /// <returns>The created appointment.</returns>
        /// <response code="201">The appointment was created successfully.</response>
        [HttpPost]
        public ActionResult<Appointment> CreateAppointment([FromBody] Appointment appointment)
        {
            appointment.Id = Appointments.Count != 0 ? Appointments.Max(a => a.Id) + 1 : 1;
            Appointments.Add(appointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
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
        public ActionResult UpdateAppointment([FromRoute] int id, [FromBody] Appointment updatedAppointment)
        {
            var appointment = Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment is null)
            {
                return NotFound();
            }

            appointment.ClientId = updatedAppointment.ClientId;
            appointment.ProviderId = updatedAppointment.ProviderId;
            appointment.ServiceId = updatedAppointment.ServiceId;
            appointment.LocationId = updatedAppointment.LocationId;
            appointment.DateTime = updatedAppointment.DateTime;
            appointment.Status = updatedAppointment.Status;

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
        public ActionResult DeleteAppointment([FromRoute] int id)
        {
            var appointment = Appointments.FirstOrDefault(a => a.Id == id);
            if (appointment is null)
            {
                return NotFound();
            }

            Appointments.Remove(appointment);
            return Ok();
        }
    }
}
