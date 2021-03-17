using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
namespace ConsultMe.Data.Repository
{
    public interface IPatientRepository
    {
        List<Patient> GetPatients(int _userId);
        bool AddPatient(Patient _patient);
        bool UpdatePatient(Patient _patient);
        bool DeletePatient(int _id);
        Patient GetPatient(int _patientId);
    }
}
