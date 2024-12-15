using ClinicAppointmentsTaskImplementation.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentsTaskImplementation.Repositories
{
    /* This class contains the CRUD (Create, Read, Update, Delete) operations
    related to the Clinics table in the database, the class implements
    IClinicRepository interface for repository pattern implementation.*/
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;
        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all clinics from the context with related data.
        public IEnumerable<Clinic> GetAllClinics()
        {
            return _context.Clinics
                .Include(c => c.ClinicAppointments) // Include appointments for the clinic.
                .ToList();
        }

        // Retrieve a single clinic by ID with related data.
        public Clinic GetClinicById(int id)
        {
            return _context.Clinics
                .Include(c => c.ClinicAppointments) // Include appointments for the clinic.
                .FirstOrDefault(c => c.clinicId == id);
        }

        public string AddClinic(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            _context.SaveChanges();
            return clinic.clinicSpec;
        }

        public void UpdateClinic(Clinic newClinic)
        {
            _context.Clinics.Update(newClinic);
            _context.SaveChanges();
        }

        public void DeleteClinic(Clinic clinic)
        {
            _context.Clinics.Remove(clinic);
            _context.SaveChanges();
        }
    }
}
