using ClinicAppointmentsTaskImplementation.DTOs;
using ClinicAppointmentsTaskImplementation.Models;
using ClinicAppointmentsTaskImplementation.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentsTaskImplementation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("Get_All_Patients")]
        public IActionResult GetAllPatients()
        {
            try
            {
                var patientsDto = _patientService.GetAllPatients();
                return Ok(patientsDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Add_Patient")]
        public IActionResult AddPatient([FromBody] PatientDTO patientDto)
        {
            try
            {
                string newPatientName = _patientService.AddPatient(patientDto);
                return Created(string.Empty, newPatientName + " Has been created.");
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpDelete("Remove_Patient/{name}")]
        public IActionResult RemovePatient(string name)
        {
            try
            {
                _patientService.DeletePatient(name);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
