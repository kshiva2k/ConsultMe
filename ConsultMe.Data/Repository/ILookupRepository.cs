using System;
using System.Collections.Generic;
using ConsultMe.Data.Models;

namespace ConsultMe.Data.Repository
{
    public interface ILookupRepository
    {
        public List<Lookupspecialist> GetSpecialistTypes();
        public List<Lookupusertype> GetUserTypes();
        public List<City> GetAllCities();
    }
}
