using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClinicAppointmentsTaskImplementation.Models
{
    public class Clinic
    {
        [Key]
        [JsonIgnore] // Ignoring this data when serializing for output.
        public int clinicId { get; set; }

        [Required]
        [MaxLength(30)]
        public string clinicSpec { get; set; }

        [Required]
        public int numberOfSlots { get; set; } = 20; // Default value set to 20

        [JsonIgnore] // Ignoring this data when serializing for output.
        public virtual ICollection<Appointment>? ClinicAppointments { get; set; }
    }
}
