using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;
using ConsultMe.Domain.ViewModels;
using ConsultMe.Service.Repository;
using Google.Protobuf.Reflection;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsultMe.Service.Services
{
    public class CustomerService : ICustomerServiceRepository
    {
        public ICustomerDataRepository customerDataRepository;
        public IClinicDataRepository clinicDataRepository;
        public ILookupRepository lookupRepository;
        public CustomerService(ICustomerDataRepository _customerDataRepository, IClinicDataRepository _clinicDataRepository, 
            ILookupRepository _lookupRepository)
        {
            customerDataRepository = _customerDataRepository;
            clinicDataRepository = _clinicDataRepository;
            lookupRepository = _lookupRepository;
        }
        public List<DoctorViewModel> GetCustomers()
        {
            var data = new List<DoctorViewModel>();
            var lstcustomers = customerDataRepository.GetCustomers();
            var specialisttype = lookupRepository.GetSpecialistTypes();
            foreach (var customer in lstcustomers)
            {
                data.Add(new DoctorViewModel()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    Email = customer.Email,
                    Mobile = customer.Mobile,
                    Specialization = customer.Specialization,
                    Specialistid = customer.Specialistid.GetValueOrDefault(),
                    Specialist = specialisttype.Where(_ => _.Id == customer.Specialistid).FirstOrDefault().Name,
                    Area = customer.Area,
                    City = customer.Cityid.GetValueOrDefault(),
                    State = customer.State,
                    Ratings = customer.Ratings.GetValueOrDefault()
                });
            }
            return data;
        }
        public List<DoctorViewModel> GetCustomersByClinic(int _clinicId)
        {
            var data = new List<DoctorViewModel>();
            var lstcustomers = customerDataRepository.GetDoctorsByClinic(_clinicId).OrderByDescending(x => x.Ratings).ToList();
            var specialisttype = lookupRepository.GetSpecialistTypes();
            foreach (var customer in lstcustomers)
            {
                data.Add(new DoctorViewModel()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    Email = customer.Email,
                    Mobile = customer.Mobile,
                    Specialization = customer.Specialization,
                    Specialistid = customer.Specialistid.GetValueOrDefault(),
                    Specialist = specialisttype.Where(_ => _.Id == customer.Specialistid).FirstOrDefault().Name,
                    Area = customer.Area,
                    City = customer.Cityid.GetValueOrDefault(),
                    State = customer.State,
                    Ratings = customer.Ratings.GetValueOrDefault()
                });
            }
            return data;
        }
        public bool AddUpdateDoctor(DoctorViewModel _customer, int _userId)
        {
            try
            {
                if (_customer.Status == 0)
                    customerDataRepository.DeleteDoctor(_customer.Id);
                else
                {
                    Doctor customer = new Doctor()
                    {
                        Name = _customer.Name,
                        Address = _customer.Address,
                        Email = _customer.Email,
                        Mobile = _customer.Mobile,
                        Specialization = _customer.Specialization,
                        Specialistid = Convert.ToInt32(_customer.Specialist),
                        Ratings = _customer.Ratings,
                        Area = _customer.Area,
                        Cityid = _customer.City,
                        State = _customer.State,
                        Status = 1
                    };
                    if (_customer.Id == 0)
                        customerDataRepository.AddDoctor(customer);
                    else
                    {
                        customer.Id = _customer.Id;
                        customerDataRepository.UpdateDoctor(customer);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool DeleteDoctor(int _id)
        {
            return customerDataRepository.DeleteDoctor(_id);
        }
        public DoctorViewModel GetDoctor(int _id)
        {
            DoctorViewModel data = new DoctorViewModel();
            var lstusers = customerDataRepository.GetCustomers();
            Doctor customer = new Doctor();
            customer = lstusers.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            data.Id = customer.Id;
            data.Name = customer.Name;
            data.Email = customer.Email;
            data.Mobile = customer.Mobile;
            data.Specialization = customer.Specialization;
            data.Specialistid = customer.Specialistid.GetValueOrDefault();
            data.Ratings = customer.Ratings.GetValueOrDefault();
            data.Subscriber = Convert.ToByte(customer.Subscriber.GetValueOrDefault());
            data.Includesearch = Convert.ToByte(customer.Includesearch.GetValueOrDefault());
            return data;
        }
        public List<string> GetCustomerAreas()
        {
            var areas = GetCustomers().Select(x => x.Area).Distinct().ToList();
            return areas;
        }
        public List<DoctorTimingsViewModel> GetCustomertimings(int _customerId, int _clinicId)
        {
            var data = new List<DoctorTimingsViewModel>();
            var list = customerDataRepository.GetDoctortimings(_customerId, _clinicId);
            var cliniclist = clinicDataRepository.GetClinics();
            var customers = GetCustomers();
            foreach(Doctortimings customertiming in list) 
            {
                data.Add(new DoctorTimingsViewModel()
                {
                    Id = customertiming.Id,
                    DoctorId = customertiming.Doctorid.GetValueOrDefault(),
                    CustomerName = customers.Where(_ => _.Id == customertiming.Doctorid.GetValueOrDefault()).FirstOrDefault().Name,
                    ClinicId = customertiming.Clinicid.GetValueOrDefault(),
                    ClinicName = cliniclist.Where(_ => _.Id == customertiming.Clinicid.GetValueOrDefault()).FirstOrDefault().Name,
                    StartTime = customertiming.Starttime.Value.ToString(),
                    EndTime = customertiming.Endtime.Value.ToString(),
                    BookingLimit = customertiming.Bookinglimit.GetValueOrDefault(),
                    ReviewTime = customertiming.Reviewtime.GetValueOrDefault()
                });
            }
            return data;
        }
        public bool AddCustomerTiming(DoctorTimingsViewModel _customerTimingsViewModel)
        {
            Doctortimings customertimings = new Doctortimings()
            {                
                Clinicid = _customerTimingsViewModel.ClinicId,
                Doctorid = _customerTimingsViewModel.DoctorId,
                Starttime = Convert.ToDecimal(_customerTimingsViewModel.StartTime),
                Endtime = Convert.ToDecimal(_customerTimingsViewModel.EndTime),
                Reviewtime = _customerTimingsViewModel.ReviewTime,
                Bookinglimit = _customerTimingsViewModel.BookingLimit
            };
            if (_customerTimingsViewModel.Id == 0)
                return customerDataRepository.AddDoctortimings(customertimings);
            else
            {
                customertimings.Id = _customerTimingsViewModel.Id;
                return customerDataRepository.UpdateDoctortimings(customertimings);
            }
        }
        public bool DeleteCustomerTiming(int _id)
        {
            return customerDataRepository.DeleteDoctortimings(_id);
        }
        public List<DoctorViewModel> SearchDoctors(string area, int specialistid)
        {
            var list = GetCustomers();
            if (!string.IsNullOrEmpty(area))
                list = list.Where(_ => _.Area.Equals(area)).ToList();
            if (specialistid > 0)
                list = list.Where(_ => _.Specialistid == specialistid).ToList();
            return list;
        }
        public DoctorViewModel GetCustomerByUser(int userId)
        {
            var data = new DoctorViewModel();
            var customer = customerDataRepository.GetCustomerByUser(userId);
            var specialisttype = lookupRepository.GetSpecialistTypes();
            if (customer != null && customer.Id > 0)
            {
                data = new DoctorViewModel()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    Email = customer.Email,
                    Mobile = customer.Mobile,
                    Specialization = customer.Specialization,
                    Specialistid = customer.Specialistid.GetValueOrDefault(),
                    Specialist = specialisttype.Where(_ => _.Id == customer.Specialistid).FirstOrDefault().Name,
                    Area = customer.Area,
                    City = customer.Cityid.GetValueOrDefault(),
                    State = customer.State,
                    Ratings = customer.Ratings.GetValueOrDefault()
                };
            }
            return data;
        }

        public List<ClinicViewModel> GetClinics()
        {
            var data = new List<ClinicViewModel>();
            var lstclinics = clinicDataRepository.GetClinics();
            foreach (var clinic in lstclinics)
            {
                data.Add(new ClinicViewModel()
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Address = clinic.Address,
                    Contactnumber = clinic.Contactnumber,
                    Area = clinic.Area,
                    City = clinic.City,
                    State = clinic.State
                });
            }
            return data;
        }
        public bool AddUpdateClinic(ClinicViewModel _clinic, int _userId)
        {
            try
            {
                if (_clinic.Status == 0)
                    clinicDataRepository.DeleteClinic(_clinic.Id);
                else
                {
                    Clinic clinic = new Clinic()
                    {
                        Name = _clinic.Name,
                        Address = _clinic.Address,
                        Contactnumber = _clinic.Contactnumber,
                        Area = _clinic.Area,
                        City = _clinic.City,
                        State = _clinic.State
                    };
                    if (_clinic.Id == 0)
                        clinicDataRepository.AddClinic(clinic);
                    else
                    {
                        clinic.Id = _clinic.Id;
                        clinicDataRepository.UpdateClinic(clinic);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool DeleteClinic(int _id)
        {
            return clinicDataRepository.DeleteClinic(_id);
        }
        public ClinicViewModel GetClinic(int _id)
        {
            ClinicViewModel data = new ClinicViewModel();
            var lstclinics = clinicDataRepository.GetClinics();
            Clinic clinic = new Clinic();
            clinic = lstclinics.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            data.Id = clinic.Id;
            data.Name = clinic.Name;
            data.Contactnumber = clinic.Contactnumber;
            data.Area = clinic.Area;
            data.City = clinic.City;
            data.State = clinic.State;
            return data;
        }
        public List<DaywiseTimingsViewModel> GetCustomerDaywiseTimingsData(int _customerId, int _clinicId, string appointmentDate)
        {
            var result = new List<DaywiseTimingsViewModel>();
            DateTime appointDate = DateTime.ParseExact(appointmentDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture);
            var timings = GetCustomertimings(_customerId, _clinicId);
            for (int index = 0; index < 7; index++)
            {
                DateTime bookingDate = appointDate.AddDays(Convert.ToDouble(index));
                result.Add(new DaywiseTimingsViewModel()
                {
                    AppointmentDate = bookingDate.ToString("dd/MM/yyyy"),
                    AppointmentDay = bookingDate.DayOfWeek.ToString(),
                    DoctorTimings = timings
                });
            }
            return result;
        }
        public List<ClinicViewModel> GetClinicsByCustomer(int _customerId)
        {
            var list = customerDataRepository.GetClinicByCustomer(_customerId);
            var data = new List<ClinicViewModel>();
            foreach (Clinic clinic in list)
            {
                data.Add(new ClinicViewModel()
                {
                    Id = clinic.Id,
                    Name = clinic.Name,
                    Address = clinic.Address,
                    Area = clinic.Area,
                    City = clinic.City,
                    Contactnumber = clinic.Contactnumber
                });
            }
            return data;
        }
        public CustomerDashboardViewModel GetCustomerDashboardData(int _customerId, DateTime _appointmentDate)
        {
            CustomerDashboardViewModel viewModel = new CustomerDashboardViewModel();

            return viewModel;
        }
    }
}
 