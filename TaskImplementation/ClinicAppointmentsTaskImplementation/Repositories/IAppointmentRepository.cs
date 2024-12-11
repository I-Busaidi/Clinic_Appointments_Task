﻿using ClinicAppointmentsTaskImplementation.Models;

namespace ClinicAppointmentsTaskImplementation.Repositories
{
    public interface IAppointmentRepository
    {
        (DateTime, string, string) AddAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAllAppointments();
        Appointment GetAppointmentById(int id);
        void UpdateAppointment(Appointment newAppointment);
    }
}