namespace ClinicAppointmentsTaskImplementation.DTOs
{
    public class AppointmentDTO
    {
        public int slotNumber { get; set; }
        public DateTime appointmentDate { get; set; }
        public string patientName { get; set; }
        public string clinicName { get; set; }

        public AppointmentDTO(int slotNumber, DateTime appointmentDate, string patientName, string clinicName) 
        {
            this.slotNumber = slotNumber;
            this.appointmentDate = appointmentDate;
            this.patientName = patientName;
            this.clinicName = clinicName;
        }
    }
}
