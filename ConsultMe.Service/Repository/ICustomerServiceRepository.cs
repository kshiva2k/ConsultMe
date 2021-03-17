using ConsultMe.Domain.ViewModels;
using System;
using System.Collections.Generic;
namespace ConsultMe.Service.Repository
{
    public interface ICustomerServiceRepository
    {
        List<DoctorViewModel> GetCustomers();
        bool AddUpdateDoctor(DoctorViewModel _customer, int _userId);
        bool DeleteDoctor(int _id);
        DoctorViewModel GetDoctor(int _id);
        List<string> GetCustomerAreas();

        List<DoctorTimingsViewModel> GetCustomertimings(int _customerId, int _clinicId);
        List<DoctorViewModel> GetCustomersByClinic(int _clinicId);
        bool AddCustomerTiming(DoctorTimingsViewModel _customerTimingsViewModel);
        bool DeleteCustomerTiming(int _id);
        List<DoctorViewModel> SearchDoctors(string area, int specialistid);
        DoctorViewModel GetCustomerByUser(int userId);

        List<ClinicViewModel> GetClinics();
        bool AddUpdateClinic(ClinicViewModel _clinic, int _userId);
        bool DeleteClinic(int _id);
        ClinicViewModel GetClinic(int _id);
        List<DaywiseTimingsViewModel> GetCustomerDaywiseTimingsData(int _customerId, int _clinicId, string appointmentDate);
        List<ClinicViewModel> GetClinicsByCustomer(int _customerId);
    }
}
