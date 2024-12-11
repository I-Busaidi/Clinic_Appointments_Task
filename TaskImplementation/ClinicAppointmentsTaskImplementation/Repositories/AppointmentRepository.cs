using ClinicAppointmentsTaskImplementation.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentsTaskImplementation.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _context.Appointments
                .Include(p => p.Patient)
                .Include(c => c.Clinic)
                .ToList();
        }

        public Appointment GetAppointmentById(int id)
        {
            return _context.Appointments
                .Include(p => p.Patient)
                .Include(c => c.Clinic)
                .FirstOrDefault(a => a.appointmentId == id);
        }

        public (DateTime, string, string) AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return (appointment.appointmentDate, appointment.Clinic.clinicSpec, appointment.Patient.patientName);
        }

        public void UpdateAppointment(Appointment newAppointment)
        {
            _context.Appointments.Update(newAppointment);
            _context.SaveChanges();
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }
    }
}
