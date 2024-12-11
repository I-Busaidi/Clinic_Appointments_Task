using ClinicAppointmentsTaskImplementation.DTOs;
using ClinicAppointmentsTaskImplementation.Models;
using ClinicAppointmentsTaskImplementation.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentsTaskImplementation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;

        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        [HttpGet("Get_All_Clinics")]
        public IActionResult GetAllClinics()
        {
            try
            {
                var clinicsDto = _clinicService.GetAllClinics();
                return Ok(clinicsDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Add_Clinic")]
        public IActionResult AddClinic([FromBody] ClinicDTO clinicDto)
        {
            try
            {
                string newClinic = _clinicService.AddClinic(clinicDto);
                return Created(string.Empty, newClinic + " clinic Has been Added");
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpDelete("Remove_Clinic/{name}")]
        public IActionResult DeleteClinic(string name)
        {
            try
            {
                _clinicService.DeleteClinic(name);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
