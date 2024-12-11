using AutoMapper;
using ClinicAppointmentsTaskImplementation.DTOs;
using ClinicAppointmentsTaskImplementation.Models;
using ClinicAppointmentsTaskImplementation.Repositories;

namespace ClinicAppointmentsTaskImplementation.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public List<PatientDTO> GetAllPatients()
        {
            var patients = _patientRepository.GetAllPatients()
                .OrderBy(p => p.patientName)
                .ToList();
            if (patients == null || patients.Count == 0)
            {
                throw new InvalidOperationException("No patients found.");
            }
            return _mapper.Map<List<PatientDTO>>(patients);
        }

        public PatientDTO GetPatientById(int id)
        {
            var patient = _patientRepository.GetPatientById(id);
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }
            return _mapper.Map<PatientDTO>(patient);
        }

        public PatientDTO GetPatientByName(string name)
        {
            var patient = _patientRepository.GetAllPatients()
                .FirstOrDefault(p => p.patientName.ToLower().Trim() == name.ToLower().Trim());
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }
            return _mapper.Map<PatientDTO>(patient);
        }

        public Patient GetPatientByNameWithNavigation(string name)
        {
            var patient = _patientRepository.GetAllPatients()
                .FirstOrDefault(p => p.patientName.ToLower().Trim() == name.ToLower().Trim());
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }

            return patient;
        }

        public string AddPatient(PatientDTO patientDto)
        {
            var patients = _patientRepository.GetAllPatients().ToList();
            if (string.IsNullOrWhiteSpace(patientDto.patientName))
            {
                throw new ArgumentException("Name is required.");
            }

            if (patients.Any(p => p.patientName.ToLower().Trim() == patientDto.patientName.ToLower().Trim()))
            {
                throw new ArgumentException("Patient with this name already exists.");
            }

            if (patientDto.patientGender.ToLower().Trim() != "male" && patientDto.patientGender.ToLower().Trim() != "female")
            {
                throw new ArgumentException("Gender is not valid.");
            }

            if (patientDto.patientAge <= 0)
            {
                throw new ArgumentException("Patient age must be entered.");
            }

            return _patientRepository.AddPatient(new Patient
            {
                patientName = patientDto.patientName,
                patientGender = patientDto.patientGender,
                patientAge = patientDto.patientAge
            });
        }

        public void UpdatePatient(string name, PatientDTO patientDto)
        {
            var currentPatient = _patientRepository.GetAllPatients().FirstOrDefault(p => p.patientName.ToLower().Trim() == name.ToLower().Trim());
            if (currentPatient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }
            if (string.IsNullOrWhiteSpace(patientDto.patientName))
            {
                throw new ArgumentException("Patient name cannot be empty.");
            }

            var patientByName = _patientRepository.GetAllPatients().FirstOrDefault(p => p.patientName.ToLower().Trim() == patientDto.patientName.ToLower().Trim());
            if (patientByName != null && patientByName.patientId != currentPatient.patientId)
            {
                throw new ArgumentException("A patient with this name already exists.");
            }

            if (patientDto.patientAge <= 0)
            {
                throw new ArgumentException("Patient age must be entered.");
            }

            if (patientDto.patientGender.ToLower().Trim() != "male" && patientDto.patientGender.ToLower().Trim() != "female")
            {
                throw new ArgumentException("Gender is not valid.");
            }

            _patientRepository.UpdatePatient(new Patient
            {
                patientName = patientDto.patientName,
                patientAge = patientDto.patientAge,
                patientGender = patientDto.patientGender,
                patientId = currentPatient.patientId
            });
        }

        public void DeletePatient(string name)
        {
            var patient = _patientRepository.GetAllPatients().FirstOrDefault(p => p.patientName.ToLower().Trim() == name.ToLower().Trim());
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient Could not be found.");
            }

            if (patient.PatientAppointments.Any())
            {
                throw new InvalidOperationException("Patient has pending appointments.");
            }

            _patientRepository.DeletePatient(patient);
        }
    }
}
