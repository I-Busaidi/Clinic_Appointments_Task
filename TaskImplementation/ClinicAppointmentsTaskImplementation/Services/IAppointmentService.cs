using ClinicAppointmentsTaskImplementation.DTOs;

namespace ClinicAppointmentsTaskImplementation.Services
{
    public interface IAppointmentService // implemented by AppointmentService class.
    {
        (DateTime, string, string) AddAppointment(string clinicName, string patientName, DateTime date);
        List<AppointmentDTO> GetAllAppointments();
        List<AppointmentDTO> GetAppointmentsByDate(DateTime date);
        List<AppointmentDTO> GetClinicAppointments(string name);
        List<AppointmentDTO> GetPatientAppointments(string name);
        void UpdateAppointmentDate(int id, DateTime date);
    }
}