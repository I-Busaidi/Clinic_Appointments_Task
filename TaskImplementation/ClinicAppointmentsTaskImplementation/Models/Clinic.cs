using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClinicAppointmentsTaskImplementation.Models
{
    public class Clinic
    {
        [Key]
        [JsonIgnore]
        public int clinicId { get; set; }

        [Required]
        [MaxLength(30)]
        public string clinicSpec { get; set; }

        [Required]
        public int numberOfSlots { get; set; } = 20;

        [JsonIgnore]
        public virtual ICollection<Appointment>? ClinicAppointments { get; set; }
    }
}
