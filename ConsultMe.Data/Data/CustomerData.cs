using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;

namespace ConsultMe.Data.Data
{
    public class CustomerData : ICustomerDataRepository
    {
        consultmeContext context { get; }
        public CustomerData(consultmeContext _context)
        {
            context = _context;
        }
        public List<Doctor> GetCustomers()
        {
            var customers = context.Doctor
                    .Where(_ => _.Status == 1)
                    .ToList();
            return customers;
        }
        public bool AddDoctor(Doctor _customer)
        {
            context.Doctor.Add(_customer);
            context.SaveChanges();
            return true;
        }
        public bool UpdateDoctor(Doctor _customer)
        {
            var customer = context.Doctor.Where(_ => _.Id.Equals(_customer.Id)).FirstOrDefault();
            customer.Name = _customer.Name;
            customer.Address = _customer.Address;
            customer.Specialistid = _customer.Specialistid;
            customer.Email = _customer.Email;
            customer.Mobile = _customer.Mobile;
            customer.Specialization = _customer.Specialization;
            customer.Ratings = _customer.Ratings;
            context.SaveChanges();
            return true;
        }
        public bool DeleteDoctor(int _id)
        {
            var customer = context.Doctor.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            customer.Status = 0;
            context.SaveChanges();
            return true;
        }

        public List<Doctortimings> GetDoctortimings(int _customerId, int _clinicId)
        {
            var data = context.Doctortimings
                    .Where(_ => _.Doctorid == _customerId && _.Clinicid == (_clinicId == 0 ? _.Clinicid : _clinicId))
                    .ToList();
            return data;
        }
        public List<Doctor> GetDoctorsByClinic(int _clinicId)
        {
            var data = (from timing in context.Doctortimings
                        join cust in context.Doctor on timing.Doctorid equals cust.Id
                        where timing.Clinicid == _clinicId && cust.Status.Value == 1
                        select cust).Distinct().ToList();
            return data;
        }
        public bool AddDoctortimings(Doctortimings _customertimings)
        {
            context.Doctortimings.Add(_customertimings);
            context.SaveChanges();
            return true;
        }
        public bool UpdateDoctortimings(Doctortimings _customertimings)
        {
            var customertiming = context.Doctortimings.Where(_ => _.Id.Equals(_customertimings.Id)).FirstOrDefault();
            customertiming.Doctorid = _customertimings.Doctorid;
            customertiming.Starttime = _customertimings.Starttime;
            customertiming.Endtime = _customertimings.Endtime;
            customertiming.Bookinglimit = _customertimings.Bookinglimit;
            customertiming.Reviewtime = _customertimings.Reviewtime;
            customertiming.Clinicid = _customertimings.Clinicid;
            context.SaveChanges();
            return true;
        }
        public bool DeleteDoctortimings(int _id)
        {
            var customertiming = context.Doctortimings.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            context.Doctortimings.Remove(customertiming);
            return true;
        }
        public List<Clinic> GetClinicByCustomer(int _customerId)
        {
            var data = (from timing in context.Doctortimings
                        join clin in context.Clinic on timing.Clinicid equals clin.Id
                        where timing.Doctorid == _customerId
                        orderby clin.Name
                        select clin).Distinct().ToList();
            return data;
        }
        public Doctor GetCustomerByUser(int _userId)
        {
            var customer = context.Doctor
                    .Where(_ => _.Status == 1 && _.Userid == _userId).FirstOrDefault();
            return customer;
        }
    }
}
