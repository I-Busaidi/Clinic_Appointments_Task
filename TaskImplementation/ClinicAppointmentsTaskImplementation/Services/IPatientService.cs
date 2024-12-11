using ClinicAppointmentsTaskImplementation.DTOs;
using ClinicAppointmentsTaskImplementation.Models;

namespace ClinicAppointmentsTaskImplementation.Services
{
    public interface IPatientService
    {
        string AddPatient(PatientDTO patientDto);
        void DeletePatient(string name);
        List<PatientDTO> GetAllPatients();
        PatientDTO GetPatientById(int id);
        PatientDTO GetPatientByName(string name);
        void UpdatePatient(string name, PatientDTO patientDto);
        Patient GetPatientByNameWithNavigation(string name);
    }
}