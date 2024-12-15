using ClinicAppointmentsTaskImplementation.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentsTaskImplementation
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // creating an index for clinic specialization with unique constraint
            modelBuilder.Entity<Clinic>()
                .HasIndex(c => c.clinicSpec)
                .IsUnique();

            // creating an index for patient name with unique constraint
            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.patientName)
                .IsUnique();
        }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
