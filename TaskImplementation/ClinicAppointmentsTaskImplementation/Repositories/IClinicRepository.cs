using ClinicAppointmentsTaskImplementation.Models;

namespace ClinicAppointmentsTaskImplementation.Repositories
{
    // Implemented by ClinicRepository
    public interface IClinicRepository
    {
        string AddClinic(Clinic clinic);
        void DeleteClinic(Clinic clinic);
        IEnumerable<Clinic> GetAllClinics();
        Clinic GetClinicById(int id);
        void UpdateClinic(Clinic newClinic);
    }
}