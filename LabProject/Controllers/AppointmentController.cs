using LabProject.Enums;
using LabProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private static List<Appointment> Appointments = new List<Appointment>
        {
            new Appointment { Id = 1, ClientId = 1, ProviderId = 1, ServiceId = 1, LocationId = 1, DateTime = DateTime.Today, Status = AppointmentStatus.Completed },
            new Appointment { Id = 2, ClientId = 2, ProviderId = 1, ServiceId = 2, LocationId = 1, DateTime = DateTime.Today.AddDays(1) }
        };

        /// <summary>
        /// Gets all appointments in the system.
        /// </summary>
        /// <returns>
        /// 200 OK – A list of all appointments.
        /// </returns>
        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            return Ok(Appointments);
        }

        /// <summary>
        /// Gets a specific appointment by ID.
        /// </summary>
        /// <param name="id">Appointment's unique identifier.</param>
        ///<returns>
        /// 200 OK – The appointment matching the ID.<br/>
        /// 404 Not Found – No appointment found with the given ID.
        /// </returns>        
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
        /// <returns>
        /// 201 Created – The created appointment with its assigned ID.
        /// </returns>
        [HttpPost]
        public ActionResult<Appointment> CreateAppointment([FromBody] Appointment appointment)
        {
            appointment.Id = Appointments.Any() ? Appointments.Max(a => a.Id) + 1 : 1;
            Appointments.Add(appointment);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to update.</param>
        /// <param name="updatedAppointment">The updated appointment data.</param>
        /// <returns>
        ///  200 OK – The updated appointment.<br/>
        /// 404 Not Found – No appointment found with the given ID.
        /// </returns>
        [HttpPut("{id}")]
        public ActionResult UpdateAppointment([FromRoute] int id,[FromBody] Appointment updatedAppointment)
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
        /// <param name="id">Appointment's unique identifier.</param>
        /// <returns>
        /// 200 Ok – Deletion successful.<br/>
        /// 404 Not Found – No appointment found with the given ID.
        /// </returns>
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
