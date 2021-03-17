using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;

namespace ConsultMe.Data.Data
{
    public class LookupData : ILookupRepository
    {
        consultmeContext context { get; }
        public LookupData(consultmeContext _context)
        {
            context = _context;
        }
        public List<Lookupspecialist> GetSpecialistTypes()
        {
            var data = context.Lookupspecialist
                    .Where(_ => _.Status == 1)
                    .ToList();
            return data;
        }
        public List<Lookupusertype> GetUserTypes()
        {
            var data = context.Lookupusertype
                    .Where(_ => _.Status == 1)
                    .ToList();
            return data;
        }
        public List<City> GetAllCities()
        {
            var data = context.City
                    .Where(_ => _.Status == 1).OrderBy(_ => _.Name)
                    .ToList();
            return data;
        }
    }
}
