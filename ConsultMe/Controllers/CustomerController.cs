using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ConsultMe.Domain.ViewModels;
using ConsultMe.Service.Repository;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace ConsultMe.Controllers
{
    public class CustomerController : Controller
    {
        public ICustomerServiceRepository customerServiceRepository;
        public IAppointmentServiceRepository appointmentServiceRepository;
        public CustomerController(ICustomerServiceRepository _customerServiceRepository, IAppointmentServiceRepository _appointmentServiceRepository)
        {
            customerServiceRepository = _customerServiceRepository;
            appointmentServiceRepository = _appointmentServiceRepository;
        }
        public IActionResult Dashboard()
        {
            int _customerid = HttpContext.Session.GetInt32("CustomerId").Value;
            var appointments = appointmentServiceRepository.GetAllAppointments(_customerid, 0);
            DateTime todate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            CustomerDashboardViewModel viewModel = new CustomerDashboardViewModel()
            {
                OverallPatientsCount = appointments.Count,
                TodayPatientCount = appointments.Where(_ => _.Appointmentdate.Equals(todate.ToShortDateString())).ToList().Count,
                TodayAppointments = appointments.Where(_ => _.Appointmentdate.Equals(todate.ToShortDateString()) && _.Status.Equals("Completed")).ToList().Count
            };
            ViewData["DashboardData"] = viewModel;
            var customer = customerServiceRepository.GetDoctor(_customerid);
            return View(customer);
        }
        public JsonResult GetClinicList()
        {
            int userid = HttpContext.Session.GetInt32("CustomerId").Value;
            var data = customerServiceRepository.GetClinicsByCustomer(userid);
            return Json(data);
        }
        public JsonResult GetClinicTiming(string clinicid)
        {
            int userid = HttpContext.Session.GetInt32("CustomerId").Value;
            var data = customerServiceRepository.GetCustomertimings(userid, Convert.ToInt32(clinicid));
            return Json(data);
        }
        public IActionResult SearchData(string clinicid, string timingid)
        {
            DateTime dtNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            int customerid = HttpContext.Session.GetInt32("CustomerId").Value;
            var data = appointmentServiceRepository.GetAllAppointments(customerid, Convert.ToInt32(clinicid), Convert.ToInt32(timingid), dtNow);
            return PartialView("_PatientList", data);
        }
        [HttpPost]
        public IActionResult PatientReview(IFormCollection form)
        {
            int _customerid = HttpContext.Session.GetInt32("CustomerId").Value;
            int appointmentid = Convert.ToInt32(form["id"].ToString());
            ViewData["Customer"] = customerServiceRepository.GetDoctor(_customerid);
            AppointmentViewModel viewModel = appointmentServiceRepository.GetAppointmentDetail(appointmentid);
            return View(viewModel);
        }
        public JsonResult UpdateAppointment(string appointmentid, string status)
        {
            var data = appointmentServiceRepository.UpdateAppointment(Convert.ToInt32(appointmentid), Convert.ToInt32(status));
            return Json(data);
        }
        public IActionResult AttDashboard()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DoctorList()
        {
            DoctorSearchListViewModel viewModel = new DoctorSearchListViewModel();
            viewModel.ClinicId = HttpContext.Session.GetInt32("CustomerId").Value;
            viewModel.Doctors = customerServiceRepository.GetCustomersByClinic(viewModel.ClinicId);
            if (viewModel.Doctors != null)
                viewModel.DoctorCount = viewModel.Doctors.Count;
            viewModel.ClinicName = customerServiceRepository.GetClinic(viewModel.ClinicId).Name;
            return View(viewModel);
        }
    }
}
