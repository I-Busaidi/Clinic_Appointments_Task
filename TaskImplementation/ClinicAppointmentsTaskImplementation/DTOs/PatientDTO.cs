namespace ClinicAppointmentsTaskImplementation.DTOs
{
    public class PatientDTO
    {
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public string patientGender { get; set; }
        public PatientDTO(string patientName, int patientAge, string patientGender) 
        {
            this.patientName = patientName;
            this.patientAge = patientAge;
            this.patientGender = patientGender;
        }
    }
}
