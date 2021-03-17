using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
namespace ConsultMe.Data.Repository
{
    public interface ICustomerDataRepository
    {
        List<Doctor> GetCustomers();
        bool AddDoctor(Doctor _customer);
        bool UpdateDoctor(Doctor _customer);
        bool DeleteDoctor(int _id);

        List<Doctortimings> GetDoctortimings(int _customerId, int _clinicId);
        List<Doctor> GetDoctorsByClinic(int _clinicId);
        bool AddDoctortimings(Doctortimings _customertimings);
        bool UpdateDoctortimings(Doctortimings _customertimings);
        bool DeleteDoctortimings(int _id);
        List<Clinic> GetClinicByCustomer(int _customerId);
        Doctor GetCustomerByUser(int _userId);
    }
}
