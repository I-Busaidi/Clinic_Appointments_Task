using ClinicAppointmentsTaskImplementation.DTOs;
using ClinicAppointmentsTaskImplementation.Models;

namespace ClinicAppointmentsTaskImplementation.Services
{
    public interface IClinicService // implemented by ClinicService class.
    {
        string AddClinic(ClinicDTO clinicDto);
        void DeleteClinic(string name);
        List<ClinicDTO> GetAllClinics();
        ClinicDTO GetClinicById(int id);
        ClinicDTO GetClinicByName(string name);
        void UpdateClinic(string name, ClinicDTO clinicDto);
        void UpdateClinicSpecialization(string currentSpec, string newSpec);
        Clinic GetClinicByNameWithRelatedData(string name);
    }
}