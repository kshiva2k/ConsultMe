using ConsultMe.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace ConsultMe.Service.Repository
{
    public interface ILookupServiceRepository
    {
        List<LookupViewModel> GetSpecialistTypes();
        List<LookupViewModel> GetUserTypes();
        List<CityViewModel> GetCities();
    }
}
