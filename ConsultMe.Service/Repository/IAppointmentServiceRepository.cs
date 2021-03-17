using System;
using System.Collections.Generic;
using ConsultMe.Domain.ViewModels;

namespace ConsultMe.Service.Repository
{
    public interface IAppointmentServiceRepository
    {
        List<AppointmentViewModel> GetAllAppointments(int _userId);
        List<AppointmentViewModel> GetAllAppointments(int _customerId, int _clinicId, int _timingId, DateTime dt);
        List<AppointmentViewModel> GetAllAppointments(int _customerId, int _clinicId);
        List<AppointmentViewModel> GetActiveAppointments(int _userId);
        long AddAppointment(AppointmentViewModel appointmentViewModel, int _userId, bool isNormalBooking);
        bool DeleteAppointment(int _id);
        AppointmentViewModel GetAppointmentDetail(int _appointmentId);
        bool UpdateAppointment(int _id, int _status);
    }
}
