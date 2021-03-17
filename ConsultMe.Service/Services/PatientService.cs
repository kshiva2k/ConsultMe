using ConsultMe.Data.Repository;
using ConsultMe.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
using ConsultMe.Service.Repository;

namespace ConsultMe.Service.Services
{
    public class PatientService : IPatientServiceRepository
    {
        public IPatientRepository patientRepository;
        public PatientService(IPatientRepository _patientRepository)
        {
            patientRepository = _patientRepository;
        }
        public List<PatientViewModel> GetPatients(int _userId)
        {
            var list = patientRepository.GetPatients(_userId).ToList();
            var result = new List<PatientViewModel>();
            foreach(Patient patient in list)
            {
                result.Add(new PatientViewModel()
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Age = patient.Age.GetValueOrDefault(),
                    RefNo = patient.Refno,
                    Gender = patient.Gender
                });
            }
            return result;
        }
        public bool AddUpdatePatient(PatientViewModel _patientViewModel)
        {
            try
            {
                Patient patient = new Patient()
                {
                    Name = _patientViewModel.Name,
                    Age = _patientViewModel.Age,
                    Gender = _patientViewModel.Gender,
                    Userid = _patientViewModel.UserId,
                    Active = 1
                };
                if (_patientViewModel.Id == 0)
                    patientRepository.AddPatient(patient);
                else
                {
                    patient.Id = _patientViewModel.Id;
                    patientRepository.AddPatient(patient);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool DeletePatient(int _id)
        {
            return patientRepository.DeletePatient(_id);
        }
    }
}
