using ConsultMe.Data.Repository;
using System;
using System.Collections.Generic;
using ConsultMe.Data.Models;
using ConsultMe.Domain.ViewModels;
using ConsultMe.Service.Repository;
using System.Linq;
using ConsultMe.Service.Enums;

namespace ConsultMe.Service.Services
{    
    public class AppointmentService : IAppointmentServiceRepository
    {
        public IAppointmentRepository appointmentRepository;
        public ICustomerDataRepository customerDataRepository;
        public IPatientRepository patientDataRepository;
        public AppointmentService(IAppointmentRepository _appointmentRepository, ICustomerDataRepository _customerDataRepository, IPatientRepository _patientDataRepository)
        {
            appointmentRepository = _appointmentRepository;
            customerDataRepository = _customerDataRepository;
            patientDataRepository = _patientDataRepository;
        }
        public List<AppointmentViewModel> GetAllAppointments(int _userId)
        {
            var patients = patientDataRepository.GetPatients(_userId);
            var result = new List<AppointmentViewModel>();
            var data = appointmentRepository.GetAppointments(_userId).OrderByDescending(_ => _.Id).ToList();
            foreach(Appointment appointment in data)
            {
                var currentAppointment = appointmentRepository.GetLiveAppointment(appointment.Timingid, appointment.Createddate.Value);
                AppointmentViewModel newdata = new AppointmentViewModel()
                {
                    Id = appointment.Id,
                    Appointmentdate = appointment.Appointmentdate.ToShortDateString(),
                    DoctorName = appointment.Doctor.Name,
                    ClinicName = appointment.Clinic.Name,
                    PatientName = patients.Where(_ => _.Id.Equals(appointment.Patientid)).FirstOrDefault().Name,
                    BookingNumber = appointment.Bookingnumber,
                    Symptoms = appointment.Symptoms,
                    SlotTime = appointment.Timing.Starttime.ToString() + " - " + appointment.Timing.Endtime.ToString(),
                    Createddate = appointment.Createddate.Value,
                    Status = ((AppointmentStatusEnum)appointment.Active.Value).ToString()
                };
                if (currentAppointment != null && currentAppointment.Id > 0)
                    newdata.CurrentReviewNumber = currentAppointment.Bookingnumber;
                result.Add(newdata);
            }
            return result;
        }
        public List<AppointmentViewModel> GetActiveAppointments(int _userId)
        {
            var result = GetAllAppointments(_userId).Where(_ => _.Status.Equals("Active")).ToList();
            return result;
        }
        public long AddAppointment(AppointmentViewModel appointmentViewModel, int _userId, bool isNormalBooking)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(appointmentViewModel.Appointmentdate);
                int bookingCount = appointmentRepository.GetAllAppointments(appointmentViewModel.CustomerId, appointmentViewModel.ClinicId, dt).Count;
                Appointment appointment = new Appointment()
                {
                    Doctorid = appointmentViewModel.CustomerId,
                    Clinicid = appointmentViewModel.ClinicId,
                    Patientid = appointmentViewModel.PatientId,
                    Bookingnumber = Convert.ToString(bookingCount + 1),
                    Timingid = Convert.ToInt32(appointmentViewModel.SlotTime),
                    Appointmentdate = dt,
                    Symptoms = appointmentViewModel.Symptoms,
                    Appointmenttype = (isNormalBooking == true ? (int)AppointmentTypeEnum.Normal : (int)AppointmentTypeEnum.Emergency),
                    Createddate = DateTime.Now,
                    Userid = _userId
                };
                return appointmentRepository.AddAppointment(appointment);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteAppointment(int _id)
        {
            return appointmentRepository.DeleteAppointment(_id);
        }
        public AppointmentViewModel GetAppointmentDetail(int _appointmentId)
        {
            Appointment appointment = appointmentRepository.GetAppointment(_appointmentId);
            var timing = customerDataRepository.GetDoctortimings(appointment.Doctorid, appointment.Clinicid.Value).Where(_ => _.Id == appointment.Timingid).FirstOrDefault();
            var patient = patientDataRepository.GetPatient(appointment.Patientid);
            AppointmentViewModel data = new AppointmentViewModel()
            {
                Id = appointment.Id,
                Appointmentdate = appointment.Appointmentdate.ToString("dd/MM/yyyy"),
                BookingNumber = appointment.Bookingnumber,
                DoctorName = appointment.Doctor.Name,
                ClinicName = appointment.Clinic.Name,
                PatientName = patient.Name,
                Age = patient.Age.Value,
                Symptoms = appointment.Symptoms,
                AppointmentType = (appointment.Appointmenttype.Value == 1 ? AppointmentTypeEnum.Normal.ToString() : AppointmentTypeEnum.Emergency.ToString()),
                SlotTime = timing.Starttime.Value.ToString()
            };
            return data;
        }
        public List<AppointmentViewModel> GetAllAppointments(int _customerId, int _clinicId, int _timingId, DateTime dt)
        {            
            var result = new List<AppointmentViewModel>();
            var data = appointmentRepository.GetAllAppointments(_customerId, _clinicId, dt)
                .Where(_ => _.Timingid == _timingId && _.Active == 1)
                .OrderByDescending(_ => _.Appointmenttype).OrderBy(_ => _.Id)
                .ToList();
            foreach (Appointment appointment in data)
            {
                var patient = patientDataRepository.GetPatient(appointment.Patientid);
                AppointmentViewModel newdata = new AppointmentViewModel()
                {
                    Id = appointment.Id,
                    Appointmentdate = appointment.Appointmentdate.ToShortDateString(),
                    DoctorName = appointment.Doctor.Name,
                    ClinicName = appointment.Clinic.Name,
                    PatientName = patient.Name,
                    BookingNumber = appointment.Bookingnumber,
                    Symptoms = appointment.Symptoms,
                    SlotTime = appointment.Timing.Starttime.ToString() + " - " + appointment.Timing.Endtime.ToString(),
                    Createddate = appointment.Createddate.Value,
                    Status = ((AppointmentStatusEnum)appointment.Active.Value).ToString()
                };
                result.Add(newdata);
            }
            return result;
        }
        public List<AppointmentViewModel> GetAllAppointments(int _customerId, int _clinicId)
        {
            var result = new List<AppointmentViewModel>();
            var data = appointmentRepository.GetAllAppointments(_customerId, _clinicId, null)
                .OrderByDescending(_ => _.Appointmenttype).OrderBy(_ => _.Id)
                .ToList();
            foreach (Appointment appointment in data)
            {
                var patient = patientDataRepository.GetPatient(appointment.Patientid);
                AppointmentViewModel newdata = new AppointmentViewModel()
                {
                    Id = appointment.Id,
                    Appointmentdate = appointment.Appointmentdate.ToShortDateString(),
                    DoctorName = appointment.Doctor.Name,
                    ClinicName = appointment.Clinic.Name,
                    PatientName = patient.Name,
                    BookingNumber = appointment.Bookingnumber,
                    Symptoms = appointment.Symptoms,
                    SlotTime = appointment.Timing.Starttime.ToString() + " - " + appointment.Timing.Endtime.ToString(),
                    Createddate = appointment.Createddate.Value,
                    Status = ((AppointmentStatusEnum)appointment.Active.Value).ToString()
                };
                result.Add(newdata);
            }
            return result;
        }
        public bool UpdateAppointment(int _id, int _status)
        {
            return appointmentRepository.UpdateAppointment(_id, _status);
        }
    }
}
