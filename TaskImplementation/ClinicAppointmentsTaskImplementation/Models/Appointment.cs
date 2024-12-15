using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace ClinicAppointmentsTaskImplementation.Models
{
    [PrimaryKey(nameof(patientId), nameof(clinicId), nameof(appointmentId))] // Composite primary key.
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Automatically generates ID numbers when a new row is added.
        public int appointmentId { get; set; }

        [Required]
        public DateTime appointmentDate { get; set; }

        [Required]
        public int slotNumber { get; set; }

        [ForeignKey("Patient")] // Foreign key from 'Patient' model.
        public int patientId { get; set; }
        [JsonIgnore] // Ignoring this data when serializing for output.
        public virtual Patient Patient { get; set; }

        [ForeignKey("Clinic")] // Foreign key from 'Clinic' model.
        public int clinicId { get; set; }
        [JsonIgnore] // Ignoring this data when serializing for output.
        public virtual Clinic Clinic { get; set; }
    }
}
