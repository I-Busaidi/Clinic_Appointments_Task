using ClinicAppointmentsTaskImplementation.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentsTaskImplementation.Repositories
{
  /* This class contains the CRUD (Create, Read, Update, Delete) operations
     related to the appointments table in the database, the class implements
     IAppointmentRepository interface for repository pattern implementation.*/

    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all the appointments data with related data.
        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _context.Appointments // Retrieve appointments from the context.
                .Include(p => p.Patient) // Include Patient data.
                .Include(c => c.Clinic)  // Include Clinic data.
                .ToList();
        }

        // Retrieve a single appointment by ID with related data.
        public Appointment GetAppointmentById(int id)
        {
            return _context.Appointments // Retrieve appointments from the context.
                .Include(p => p.Patient) // Include Patient data.
                .Include(c => c.Clinic)  // Include Clinic data.
                .FirstOrDefault(a => a.appointmentId == id); // Returns the first value found that matches the data, or data type if none was found.
        }

        public (DateTime, string, string) AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment); // Add apointment to the context.
            _context.SaveChanges(); // Applying the changes to the database.
            return (appointment.appointmentDate, appointment.Clinic.clinicSpec, appointment.Patient.patientName);
        }

        public void UpdateAppointment(Appointment newAppointment)
        {
            _context.Appointments.Update(newAppointment); // Update the row that has the same ID with new data.
            _context.SaveChanges(); // Applying the changes to the database.
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _context.Appointments.Remove(appointment); // Remove the given appointment from context.
            _context.SaveChanges(); // Applying the changes to the database.
        }
    }
}
