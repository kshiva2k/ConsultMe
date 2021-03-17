using System;
using System.Collections.Generic;
using ConsultMe.Domain.ViewModels;

namespace ConsultMe.Service.Repository
{
    public interface IPatientServiceRepository
    {
        List<PatientViewModel> GetPatients(int _userId);
        bool AddUpdatePatient(PatientViewModel _patientViewModel);
        bool DeletePatient(int _id);
    }
}
