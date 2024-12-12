using AutoMapper;
using ClinicAppointmentsTaskImplementation.DTOs;
using ClinicAppointmentsTaskImplementation.Models;
using ClinicAppointmentsTaskImplementation.Repositories;

namespace ClinicAppointmentsTaskImplementation.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IClinicService _clinicService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

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
                .OrderBy(ap => ap.appointmentDate)
                .Select(a => new AppointmentDTO
                (
                    a.slotNumber,
                    a.appointmentDate,
                    a.Patient.patientName,
                    a.Clinic.clinicSpec
                    ))
                .ToList();

            if (appointments == null || appointments.Count == 0)
            {
                throw new InvalidOperationException("No appointments found.");
            }
            return appointments;
        }

        public List<AppointmentDTO> GetAppointmentsByDate(DateTime date)
        {
            var appointments = _appointmentRepository.GetAllAppointments()
                .OrderBy(ap => ap.appointmentDate)
                .Where(ap => ap.appointmentDate.Date == date.Date)
                .Select(a => new AppointmentDTO
                (
                    a.slotNumber,
                    a.appointmentDate,
                    a.Patient.patientName,
                    a.Clinic.clinicSpec
                    ))
                .ToList();

            if (appointments == null || appointments.Count == 0)
            {
                throw new InvalidOperationException("No appointments found on this date.");
            }
            return appointments;
        }

        public List<AppointmentDTO> GetPatientAppointments(string name)
        {
            var patient = _patientService.GetPatientByNameWithRelatedData(name);
            if (patient.PatientAppointments == null || patient.PatientAppointments.Count == 0)
            {
                throw new InvalidOperationException($"{name} has no appointments.");
            }
            return patient.PatientAppointments
                .OrderBy(ap => ap.appointmentDate)
                .Select(a => new AppointmentDTO
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
            var clinic = _clinicService.GetClinicByNameWithRelatedData(name);
            if (clinic.ClinicAppointments == null || clinic.ClinicAppointments.Count == 0)
            {
                throw new InvalidOperationException($"{name} clinic has no appointments currently.");
            }
            return clinic.ClinicAppointments
                .OrderBy(ap => ap.appointmentDate)
                .Select(a => new AppointmentDTO
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
            var clinic = _clinicService.GetClinicByNameWithRelatedData(clinicName);
            var patient = _patientService.GetPatientByNameWithRelatedData(patientName);

            int appointmentCount = clinic.ClinicAppointments.Count(ap => ap.appointmentDate.Date == date.Date);

            if (appointmentCount >= clinic.numberOfSlots)
            {
                throw new ArgumentException($"No slots available for this date in {clinicName} clinic.");
            }

            if (patient.PatientAppointments != null || patient.PatientAppointments.Count > 0)
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
            var clinic = _clinicService.GetClinicByNameWithRelatedData(appointment.Clinic.clinicSpec);
            var patient = _patientService.GetPatientByNameWithRelatedData(appointment.Patient.patientName);

            int appointmentSlot = clinic.ClinicAppointments.Count(a => a.appointmentDate.Date == date.Date) + 1;

            if (appointmentSlot > clinic.numberOfSlots)
            {
                throw new ArgumentException($"No slots available for this date in {clinic.clinicSpec} clinic.");
            }

            if (patient.PatientAppointments != null || patient.PatientAppointments.Count > 0)
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
