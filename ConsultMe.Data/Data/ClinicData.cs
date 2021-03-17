using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsultMe.Data.Data
{
    public class ClinicData : IClinicDataRepository
    {
        consultmeContext context { get; }
        public ClinicData(consultmeContext _context)
        {
            context = _context;
        }
        public List<Clinic> GetClinics()
        {
            var clinics = context.Clinic
                    .Where(_ => _.Active == 1)
                    .ToList();
            return clinics;
        }
        public bool AddClinic(Clinic _clinic)
        {
            context.Clinic.Add(_clinic);
            context.SaveChanges();
            return true;
        }
        public bool UpdateClinic(Clinic _clinic)
        {
            var clinic = context.Clinic.Where(_ => _.Id.Equals(_clinic.Id)).FirstOrDefault();
            clinic.Name = _clinic.Name;
            clinic.Address = _clinic.Address;
            clinic.Area = _clinic.Area;
            clinic.City = _clinic.City;
            clinic.Contactnumber = _clinic.Contactnumber;
            clinic.State = _clinic.State;
            context.SaveChanges();
            return true;
        }
        public bool DeleteClinic(int _id)
        {
            var clinic = context.Clinic.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            clinic.Active = 0;
            context.SaveChanges();
            return true;
        }
    }
}
