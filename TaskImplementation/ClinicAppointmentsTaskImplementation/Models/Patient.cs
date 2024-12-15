using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClinicAppointmentsTaskImplementation.Models
{
    public class Patient
    {
        [Key]
        [JsonIgnore] // Ignoring this data when serializing for output.
        public int patientId { get; set; }

        [Required]
        [MaxLength(50)]
        public string patientName { get; set; }

        [Required]
        public int patientAge { get; set; }

        [Required]
        [MaxLength(7)]
        public string patientGender { get; set; }

        [JsonIgnore] // Ignoring this data when serializing for output.
        public virtual ICollection<Appointment>? PatientAppointments { get; set; }
    }
}
