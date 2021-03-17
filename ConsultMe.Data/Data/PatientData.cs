using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;

namespace ConsultMe.Data.Data
{
    public class PatientData : IPatientRepository
    {
        consultmeContext context { get; }
        public PatientData(consultmeContext _context)
        {
            context = _context;
        }
        public List<Patient> GetPatients(int _userId)
        {
            var patients = context.Patient
                    .Where(_ => _.Active == 1 && _.Userid == (_userId != 0 ? _userId : _.Userid))
                    .ToList();
            return patients;
        }
        public bool AddPatient(Patient _patient)
        {
            _patient.Refno = GenerateRefNo();
            context.Patient.Add(_patient);
            context.SaveChanges();
            return true;
        }
        public bool UpdatePatient(Patient _patient)
        {
            var patient = context.Patient.Where(_ => _.Id.Equals(_patient.Id)).FirstOrDefault();
            patient.Name = _patient.Name;
            patient.Age = _patient.Age;
            patient.Gender = _patient.Gender;
            context.SaveChanges();
            return true;
        }
        public bool DeletePatient(int _id)
        {
            var patient = context.Patient.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            patient.Active = 0;
            context.SaveChanges();
            return true;
        }
        public Patient GetPatient(int _patientId)
        {
            var patient = context.Patient
                    .Where(_ => _.Active == 1 && _.Id == _patientId)
                    .FirstOrDefault();
            return patient;
        }
        private string GenerateRefNo()
        {
            string refNo = context.Patient.Where(x => x.Active == 1).Select(x => x.Refno).Max();
            if (string.IsNullOrEmpty(refNo))
                refNo = "PT0001";
            else
            {
                int number = Convert.ToInt32(refNo.Substring(2, refNo.Length - 1));
                refNo = "PT" + (number + 1).ToString("D4");
            }
            return refNo;
        }
    }
}
