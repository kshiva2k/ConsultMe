using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
namespace ConsultMe.Data.Repository
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAppointments(int _userId);
        List<Appointment> GetAllAppointments(int _customerId, int _clinicId, DateTime? date);
        long AddAppointment(Appointment _appointment);
        bool DeleteAppointment(int _id);
        Appointment GetAppointment(int _id);
        Appointment GetLiveAppointment(int _timingId, DateTime date);
        bool UpdateAppointment(int _id, int _status);
    }
}
