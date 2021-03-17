using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace ConsultMe.Data.Data
{
    public class AppointmentData : IAppointmentRepository
    {
        consultmeContext context { get; }
        public AppointmentData(consultmeContext _context)
        {
            context = _context;
        }
        public List<Appointment> GetAppointments(int _userId)
        {
            var appointments = context.Appointment
                    .Include(c => c.Doctor).Include(cl => cl.Clinic)//.Include(ct => ct.Timing)
                    .Where(_ => _.Active != 0 && _.Userid == _userId)
                    .ToList();
            return appointments;
        }
        public List<Appointment> GetAllAppointments(int _customerId, int _clinicId, DateTime? date)
        {
            var appointments = context.Appointment
                    .Include(cu => cu.Doctor)
                    .Include(cl => cl.Clinic)
                    .Include(ct => ct.Timing)
                    .Where(_ => _.Active != 0 && _.Doctorid == _customerId && _.Clinicid == (_clinicId != 0 ? _clinicId : _.Clinicid)
                        && _.Appointmentdate == (date != null ? date.Value : _.Appointmentdate))
                    .ToList();
            return appointments;
        }
        public long AddAppointment(Appointment _appointment)
        {
            context.Appointment.Add(_appointment);
            context.SaveChanges();
            return _appointment.Id;
        }
        public bool DeleteAppointment(int _id)
        {
            var appointment = context.Appointment.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            appointment.Active = 0;
            context.SaveChanges();
            return true;
        }
        public Appointment GetAppointment(int _id)
        {
            var appointment = context.Appointment
                    .Where(_ => _.Id == _id)
                    .Include(c => c.Doctor).Include(cl => cl.Clinic)
                    .FirstOrDefault();
            return appointment;
        }
        public Appointment GetLiveAppointment(int _timingId, DateTime date)
        {
            var appointments = context.Appointment
                    .Where(_ => _.Active != 0 && _.Timingid == _timingId && (_.Appointmentdate > date && _.Appointmentdate < date.AddDays(1.0))
                        && _.Active == 2)
                    .FirstOrDefault();
            return appointments;
        }
        public bool UpdateAppointment(int _id, int _status)
        {
            var appointment = context.Appointment.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            appointment.Active = _status;
            context.SaveChanges();
            return true;
        }
    }
}
