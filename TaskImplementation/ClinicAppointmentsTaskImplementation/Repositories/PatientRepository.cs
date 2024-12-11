using ClinicAppointmentsTaskImplementation.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentsTaskImplementation.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _context.Patients.Include(p => p.PatientAppointments).ToList();
        }

        public Patient GetPatientById(int id)
        {
            return _context.Patients.Include(p => p.PatientAppointments)
                .FirstOrDefault(p => p.patientId == id);
        }

        public string AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient.patientName;
        }

        public void UpdatePatient(Patient newPatient)
        {
            _context.Patients.Update(newPatient);
            _context.SaveChanges();
        }

        public void DeletePatient(Patient patient)
        {
            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }
    }
}
