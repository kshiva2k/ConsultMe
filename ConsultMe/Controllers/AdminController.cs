using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Domain.ViewModels;
using ConsultMe.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultMe.Controllers
{
    public class AdminController : Controller
    {
        public ICustomerServiceRepository customerServiceRepository;
        public ILookupServiceRepository lookupServiceRepository;
        public IUserServiceRepository userServiceRepository;
        public AdminController(ICustomerServiceRepository _customerServiceRepository, ILookupServiceRepository _lookupServiceRepository,
           IUserServiceRepository _userServiceRepository)
        {
            customerServiceRepository = _customerServiceRepository;
            lookupServiceRepository = _lookupServiceRepository;
            userServiceRepository = _userServiceRepository;
        }
        public IActionResult AdminDashboard()
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            AdminDashboardViewModel viewModel = userServiceRepository.GetAdminDashboardData();
            return View(viewModel);
        }
        public IActionResult CustomerList()
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            return View();
        }
        public IActionResult GetVendors()
        {
            List<DoctorViewModel> list = customerServiceRepository.GetCustomers();
            return PartialView("_Vendors", list);
        }
        public IActionResult AddVendor()
        {
            ViewData["PageTitle"] = "Add Doctor";
            ViewData["Specialists"] = lookupServiceRepository.GetSpecialistTypes();
            DoctorViewModel customermodel = new DoctorViewModel();
            return PartialView("_CustomerPage", customermodel);
        }
        public IActionResult EditVendor(string id)
        {
            ViewData["PageTitle"] = "Update Doctor";
            ViewData["Specialists"] = lookupServiceRepository.GetSpecialistTypes();
            DoctorViewModel customermodel = customerServiceRepository.GetDoctor(Convert.ToInt32(id));
            return PartialView("_CustomerPage", customermodel);
        }
        public JsonResult SaveVendor(DoctorViewModel model)
        {
            model.Status = 1;
            bool result = customerServiceRepository.AddUpdateDoctor(model, model.Id);
            return new JsonResult(result);
        }

        public IActionResult ClinicList()
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            return View();
        }
        public IActionResult GetClinics()
        {
            List<ClinicViewModel> list = customerServiceRepository.GetClinics();
            return PartialView("_Clinics", list);
        }
        public IActionResult AddClinic()
        {
            ViewData["PageTitle"] = "Add Clinic";
            ClinicViewModel clinicmodel = new ClinicViewModel();
            return PartialView("_ClinicPage", clinicmodel);
        }
        public IActionResult EditClinic(string id)
        {
            ViewData["PageTitle"] = "Update Clinic";
            ClinicViewModel clinicmodel = customerServiceRepository.GetClinic(Convert.ToInt32(id));
            return PartialView("_ClinicPage", clinicmodel);
        }
        public JsonResult SaveClinic(ClinicViewModel model)
        {
            model.Status = 1;
            bool result = customerServiceRepository.AddUpdateClinic(model, model.Id);
            return new JsonResult(result);
        }

        public IActionResult DoctorTimings(string id)
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");            
            DoctorTimingsViewModel viewModel = new DoctorTimingsViewModel()
            {
                DoctorId = Convert.ToInt32(id)
            };
            return View(viewModel);
        }
        public IActionResult GetDoctorTimingForm()
        {
            ViewData["Clinics"] = customerServiceRepository.GetClinics();
            DoctorTimingsViewModel viewModel = new DoctorTimingsViewModel();
            return PartialView("_CustomerTimings", viewModel);
        }
        public IActionResult GetTimings(string _customerId)
        {
            List<DoctorTimingsViewModel> list = customerServiceRepository.GetCustomertimings(Convert.ToInt32(_customerId), 0);
            return PartialView("_CustomerTimingsList", list);
        }
        public JsonResult SaveTimings(DoctorTimingsViewModel model)
        {
            bool result = customerServiceRepository.AddCustomerTiming(model);
            var data = new { result = result, id = model.DoctorId };
            return new JsonResult(data);
        }
    }
}
