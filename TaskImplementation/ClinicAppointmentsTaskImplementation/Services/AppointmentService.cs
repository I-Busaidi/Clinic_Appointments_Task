using AutoMapper;
using ClinicAppointmentsTaskImplementation.DTOs;
using ClinicAppointmentsTaskImplementation.Models;
using ClinicAppointmentsTaskImplementation.Repositories;

namespace ClinicAppointmentsTaskImplementation.Services
{
    // Implements IAppointmentService interface.
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IClinicService _clinicService; // Used for retrieving clinic data whem making a new appointment.
        private readonly IPatientService _patientService; // Used for retrieving patient data whem making a new appointment.
        private readonly IMapper _mapper; // Used for mapping DTOs.

        public AppointmentService(IAppointmentRepository appointmentRepository, IClinicService clinicService, IPatientService patientService, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _clinicService = clinicService;
            _patientService = patientService;
            _mapper = mapper;
        }

        public List<AppointmentDTO> GetAllAppointments()
        {
            var appointments = _appointmentRepository.GetAllAppointments()
                .OrderBy(ap => ap.appointmentDate) // Ordering appointments based on date, in ascending order.
                .Select(a => new AppointmentDTO // Creating a DTO to be transferred to the controller instead of sending the appointment entity itself.
                (
                    a.slotNumber,
                    a.appointmentDate,
                    a.Patient.patientName,
                    a.Clinic.clinicSpec
                    ))
                .ToList();

            // Throw an exception if no appointments found.
            if (appointments == null || appointments.Count == 0)
            {
                throw new InvalidOperationException("No appointments found.");
            }
            return appointments;
        }

        public List<AppointmentDTO> GetAppointmentsByDate(DateTime date)
        {
            var appointments = _appointmentRepository.GetAllAppointments()
                .OrderBy(ap => ap.appointmentDate) // Ordering appointments based on date, in ascending order.
                .Where(ap => ap.appointmentDate.Date == date.Date) // Retrieve appointments with a date the matches the parameter date.
                .Select(a => new AppointmentDTO // Creating a DTO to be transferred to the controller instead of sending the appointment entity itself.
                (
                    a.slotNumber,
                    a.appointmentDate,
                    a.Patient.patientName,
                    a.Clinic.clinicSpec
                    ))
                .ToList();

            // Throw an exception if no appointments found.
            if (appointments == null || appointments.Count == 0)
            {
                throw new InvalidOperationException("No appointments found on this date.");
            }
            return appointments;
        }

        public List<AppointmentDTO> GetPatientAppointments(string name)
        {
            // Retrieve the patient data with related appointments.
            var patient = _patientService.GetPatientByNameWithRelatedData(name);
            if (patient.PatientAppointments == null || patient.PatientAppointments.Count == 0)
            {
                throw new InvalidOperationException($"{name} has no appointments.");
            }
            return patient.PatientAppointments
                .OrderBy(ap => ap.appointmentDate) // Ordering appointments based on date, in ascending order.
                .Select(a => new AppointmentDTO // Creating a DTO to be transferred to the controller instead of sending the appointment entity itself.
                (
                    a.slotNumber,
                    a.appointmentDate,
                    a.Patient.patientName,
                    a.Clinic.clinicSpec
                    ))
                .ToList();
        }

        public List<AppointmentDTO> GetClinicAppointments(string name)
        {
            // Retrieve the clinic data with related appointments.
            var clinic = _clinicService.GetClinicByNameWithRelatedData(name);
            if (clinic.ClinicAppointments == null || clinic.ClinicAppointments.Count == 0)
            {
                throw new InvalidOperationException($"{name} clinic has no appointments currently.");
            }
            return clinic.ClinicAppointments
                .OrderBy(ap => ap.appointmentDate) // Ordering appointments based on date, in ascending order.
                .Select(a => new AppointmentDTO // Creating a DTO to be transferred to the controller instead of sending the appointment entity itself.
                (
                    a.slotNumber,
                    a.appointmentDate,
                    a.Patient.patientName,
                    a.Clinic.clinicSpec
                    ))
                .ToList();
        }

        public (DateTime, string, string) AddAppointment(string clinicName, string patientName, DateTime date)
        {
            var clinic = _clinicService.GetClinicByNameWithRelatedData(clinicName); // getting the clinic along with related data.
            var patient = _patientService.GetPatientByNameWithRelatedData(patientName); // getting the patient along with related data.

            int appointmentCount = clinic.ClinicAppointments.Count(ap => ap.appointmentDate.Date == date.Date); // getting the number of slots taken in the given date.

            if (appointmentCount >= clinic.numberOfSlots) //if no slots available.
            {
                throw new ArgumentException($"No slots available for this date in {clinicName} clinic.");
            }

            if (patient.PatientAppointments != null || patient.PatientAppointments.Count > 0) // if patient has appointments already,
                                                                                              // check if they have an appointment in the same clinic on the same day.
            {
                if (patient.PatientAppointments.Any(ap => ap.appointmentDate.Date == date.Date && ap.clinicId == clinic.clinicId))
                {
                    throw new InvalidOperationException($"Patient {patientName} already has an appointment in {clinicName} clinic on this date.");
                }
            }

            var createdAppointment = _appointmentRepository.AddAppointment(new Appointment
            {
                appointmentDate = date.Date,
                slotNumber = appointmentCount + 1,
                clinicId = clinic.clinicId,
                patientId = patient.patientId
            });

            return createdAppointment;
        }

        public void UpdateAppointmentDate(int id, DateTime date)
        {
            var appointment = _appointmentRepository.GetAppointmentById(id);
            var clinic = _clinicService.GetClinicByNameWithRelatedData(appointment.Clinic.clinicSpec);// getting the clinic along with related data.
            var patient = _patientService.GetPatientByNameWithRelatedData(appointment.Patient.patientName);// getting the patient along with related data.

            int appointmentSlot = clinic.ClinicAppointments.Count(a => a.appointmentDate.Date == date.Date) + 1; // getting the number of slots taken in the given date.

            if (appointmentSlot > clinic.numberOfSlots) // if the slots limit reached for thr given date.
            {
                throw new ArgumentException($"No slots available for this date in {clinic.clinicSpec} clinic.");
            }

            if (patient.PatientAppointments != null || patient.PatientAppointments.Count > 0) // if patient has appointments already,
                                                                                              // check if they have an appointment in the same clinic on the same day.
            {
                if (patient.PatientAppointments.Any(ap => ap.appointmentDate.Date == date.Date && ap.clinicId == clinic.clinicId))
                {
                    throw new InvalidOperationException($"Patient {patient.patientName} already has an appointment in {clinic.clinicSpec} clinic on this date.");
                }
            }

            appointment.appointmentDate = date;
            appointment.slotNumber = appointmentSlot;
            _appointmentRepository.UpdateAppointment(appointment);
        }
    }
}
