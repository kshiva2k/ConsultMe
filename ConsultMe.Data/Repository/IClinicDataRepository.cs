using System;
using System.Collections.Generic;
using ConsultMe.Data.Models;

namespace ConsultMe.Data.Repository
{
    public interface IClinicDataRepository
    {
        public List<Clinic> GetClinics();
        public bool AddClinic(Clinic _clinic);
        public bool UpdateClinic(Clinic _clinic);
        public bool DeleteClinic(int _id);
    }
}
