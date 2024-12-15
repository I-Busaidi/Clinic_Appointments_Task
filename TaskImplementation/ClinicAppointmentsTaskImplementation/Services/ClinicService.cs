using AutoMapper;
using ClinicAppointmentsTaskImplementation.DTOs;
using ClinicAppointmentsTaskImplementation.Models;
using ClinicAppointmentsTaskImplementation.Repositories;

namespace ClinicAppointmentsTaskImplementation.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly IMapper _mapper;
        public ClinicService(IClinicRepository clinicRepository, IMapper mapper)
        {
            _clinicRepository = clinicRepository;
            _mapper = mapper;
        }

        public List<ClinicDTO> GetAllClinics()
        {
            var clinics = _clinicRepository.GetAllClinics()
                .OrderBy(c => c.clinicSpec) // order by date in ascending order.
                .ToList();
            if (clinics == null || clinics.Count == 0)
            {
                throw new InvalidOperationException("No clinics found.");
            }
            return _mapper.Map<List<ClinicDTO>>(clinics);
        }

        public ClinicDTO GetClinicById(int id)
        {
            var clinic = _clinicRepository.GetClinicById(id);
            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic with this ID.");
            }
            return _mapper.Map<ClinicDTO>(clinic);
        }

        public ClinicDTO GetClinicByName(string name)
        {
            var clinic = _clinicRepository.GetAllClinics()
                .FirstOrDefault(c => c.clinicSpec.ToLower().Trim() == name.ToLower().Trim()); //trim extra spaces and switch to lowecase for better comparison.

            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic with this name.");
            }

            return _mapper.Map<ClinicDTO>(clinic);
        }

        public Clinic GetClinicByNameWithRelatedData(string name) // used to get clinic along with related data.
        {
            var clinic = _clinicRepository.GetAllClinics()
                .FirstOrDefault(c => c.clinicSpec.ToLower().Trim() == name.ToLower().Trim());

            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic with this name.");
            }

            return clinic;
        }

        public string AddClinic(ClinicDTO clinicDto)
        {
            var clinics = _clinicRepository.GetAllClinics().ToList();

            // if a clinic with the same specialization already exists.
            if (clinics.Any(c => c.clinicSpec.ToLower().Trim() == clinicDto.clinicSpec.ToLower().Trim())) 
            {
                throw new ArgumentException("Clinic with this specialization already exists.");
            }
            // if clinic specialization is empty
            if (string.IsNullOrWhiteSpace(clinicDto.clinicSpec))
            {
                throw new ArgumentException("Clinic specialization is required.");
            }

            return _clinicRepository.AddClinic(new Clinic
            {
                clinicSpec = clinicDto.clinicSpec,
                numberOfSlots = clinicDto.numberOfSlots
            });
        }

        public void UpdateClinic(string name, ClinicDTO clinicDto)
        {
            var existingClinic = _clinicRepository.GetAllClinics()
                .FirstOrDefault(c => c.clinicSpec.ToLower().Trim() == name.ToLower().Trim());
            var clinicByName = _clinicRepository.GetAllClinics()
                .FirstOrDefault(c => c.clinicSpec.ToLower().Trim() == clinicDto.clinicSpec.ToLower().Trim());
            if (existingClinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic.");
            }

            if (string.IsNullOrWhiteSpace(clinicDto.clinicSpec))
            {
                throw new ArgumentException("Clinic Specialization is required.");
            }

            //if the updated clinic specialization is used by a different clinic
            if (clinicByName != null && clinicByName.clinicId != existingClinic.clinicId)
            {
                throw new ArgumentException("A clinic with this specialization already exists.");
            }

            if (clinicDto.numberOfSlots <= 0)
            {
                throw new ArgumentException("Number of appointment slots must be greater than 0.");
            }

            _clinicRepository.UpdateClinic(new Clinic
            {
                clinicId = existingClinic.clinicId,
                clinicSpec = clinicDto.clinicSpec,
                numberOfSlots = clinicDto.numberOfSlots
            });
        }

        public void DeleteClinic(string name)
        {
            var clinic = _clinicRepository.GetAllClinics()
                .FirstOrDefault(c => c.clinicSpec.ToLower().Trim() == name.ToLower().Trim());
            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic.");
            }

            // if clinic has appointments booked.
            if (clinic.ClinicAppointments.Any())
            {
                throw new InvalidOperationException("Clinic has pending appointments.");
            }

            _clinicRepository.DeleteClinic(clinic);
        }

        public void UpdateClinicSpecialization(string currentSpec, string newSpec)
        {
            var clinic = _clinicRepository.GetAllClinics()
                .FirstOrDefault(c => c.clinicSpec.ToLower().Trim() == currentSpec.ToLower().Trim());
            if (string.IsNullOrWhiteSpace(newSpec))
            {
                throw new ArgumentException("Clinic specialization cannot be empty.");
            }
            var clinicByName = _clinicRepository.GetAllClinics().FirstOrDefault(c => c.clinicSpec.ToLower().Trim() == newSpec.ToLower().Trim());
            if (clinic == null)
            {
                throw new KeyNotFoundException("Could not find clinic.");
            }

            //if the updated clinic specialization is used by a different clinic
            if (clinicByName != null && clinicByName.clinicId != clinic.clinicId)
            {
                throw new ArgumentException("A clinic with this specialization already exists.");
            }

            clinic.clinicSpec = newSpec;
            _clinicRepository.UpdateClinic(clinic);
        }
    }
}
