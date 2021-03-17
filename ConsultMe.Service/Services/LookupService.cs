using ConsultMe.Data.Repository;
using ConsultMe.Domain.ViewModels;
using ConsultMe.Service.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultMe.Service.Services
{    
    public class LookupService : ILookupServiceRepository
    {
        public ILookupRepository lookupRepository;
        public LookupService(ILookupRepository _lookupRepository)
        {
            lookupRepository = _lookupRepository;
        }
        public List<LookupViewModel> GetSpecialistTypes()
        {
            var data = lookupRepository.GetSpecialistTypes();
            var result = new List<LookupViewModel>();
            foreach(var item in data)
            {
                result.Add(new LookupViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return result;
        }
        public List<LookupViewModel> GetUserTypes()
        {
            var data = lookupRepository.GetUserTypes();
            var result = new List<LookupViewModel>();
            foreach (var item in data)
            {
                result.Add(new LookupViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return result;
        }
        public List<CityViewModel> GetCities()
        {
            var data = lookupRepository.GetAllCities();
            var result = new List<CityViewModel>();
            foreach (var item in data)
            {
                result.Add(new CityViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return result;
        }
    }
}
