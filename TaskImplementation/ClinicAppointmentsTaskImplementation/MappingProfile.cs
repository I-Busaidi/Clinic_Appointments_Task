using AutoMapper;
using ClinicAppointmentsTaskImplementation.DTOs;
using ClinicAppointmentsTaskImplementation.Models;
namespace ClinicAppointmentsTaskImplementation
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Patient, PatientDTO>();
        }
    }
}
