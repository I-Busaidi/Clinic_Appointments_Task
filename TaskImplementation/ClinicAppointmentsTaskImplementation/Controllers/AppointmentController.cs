using ClinicAppointmentsTaskImplementation.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentsTaskImplementation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("Get_All_Appointments")]
        public IActionResult GetAllAppointments()
        {
            try
            {
                var appointmentsDto = _appointmentService.GetAllAppointments();
                return Ok(appointmentsDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Get_Appointment_By_Patient/{name}")]
        public IActionResult GetPatientAppointments(string name)
        {
            try
            {
                var appointmentsDto = _appointmentService.GetPatientAppointments(name);
                return Ok(appointmentsDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Get_Appointment_By_Clinic/{cSpecialization}")]
        public IActionResult GetClinicAppointments(string cSpecialization)
        {
            try
            {
                var appointmentsDto = _appointmentService.GetClinicAppointments(cSpecialization);
                return Ok(appointmentsDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Add_Clinic")]
        public IActionResult BookAppointment(string clinicName, string patientName, DateTime date)
        {
            try
            {
                var appointmentDetails = _appointmentService.AddAppointment(clinicName, patientName, date);
                return Created(string.Empty, $"Appointment for {appointmentDetails.Item3} in {appointmentDetails.Item2} clinic on {appointmentDetails.Item1} has been created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
