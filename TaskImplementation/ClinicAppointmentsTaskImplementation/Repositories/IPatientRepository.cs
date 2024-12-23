﻿using ClinicAppointmentsTaskImplementation.Models;

namespace ClinicAppointmentsTaskImplementation.Repositories
{
    // Implemented by PatientRepository
    public interface IPatientRepository
    {
        string AddPatient(Patient patient);
        void DeletePatient(Patient patient);
        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(int id);
        void UpdatePatient(Patient newPatient);
    }
}