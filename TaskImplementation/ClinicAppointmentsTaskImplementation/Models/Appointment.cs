using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace ClinicAppointmentsTaskImplementation.Models
{
    [PrimaryKey(nameof(patientId), nameof(clinicId), nameof(appointmentId))]
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int appointmentId { get; set; }

        [Required]
        public DateTime appointmentDate { get; set; }

        [Required]
        public int slotNumber { get; set; }

        [ForeignKey("Patient")]
        public int patientId { get; set; }
        [JsonIgnore]
        public virtual Patient Patient { get; set; }

        [ForeignKey("Clinic")]
        public int clinicId { get; set; }
        [JsonIgnore]
        public virtual Clinic Clinic { get; set; }
    }
}
