namespace ClinicAppointmentsTaskImplementation.DTOs
{
    public class ClinicDTO
    {
        public string clinicSpec {  get; set; }
        public int numberOfSlots { get; set; }

        public ClinicDTO(string clinicSpec, int numberOfSlots) 
        {
            this.clinicSpec = clinicSpec;
            this.numberOfSlots = numberOfSlots;
        }
    }
}
