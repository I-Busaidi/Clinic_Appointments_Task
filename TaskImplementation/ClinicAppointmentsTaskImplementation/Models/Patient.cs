using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClinicAppointmentsTaskImplementation.Models
{
    public class Patient
    {
        [Key]
        [JsonIgnore]
        public int patientId { get; set; }

        [Required]
        [MaxLength(50)]
        public string patientName { get; set; }

        [Required]
        public int patientAge { get; set; }

        [Required]
        [MaxLength(7)]
        public string patientGender { get; set; }

        [JsonIgnore]
        public virtual ICollection<Appointment>? PatientAppointments { get; set; }
    }
}
