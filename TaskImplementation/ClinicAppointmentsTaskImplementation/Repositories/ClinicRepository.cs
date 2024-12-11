using ClinicAppointmentsTaskImplementation.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentsTaskImplementation.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;
        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Clinic> GetAllClinics()
        {
            return _context.Clinics.Include(c => c.ClinicAppointments).ToList();
        }

        public Clinic GetClinicById(int id)
        {
            return _context.Clinics.Include(c => c.ClinicAppointments).FirstOrDefault(c => c.clinicId == id);
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
