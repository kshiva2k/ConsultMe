using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultMe.Domain.ViewModels;
using ConsultMe.Service.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultMe.Controllers
{
    public class UserController : Controller
    {
        public ICustomerServiceRepository customerServiceRepository;
        public ILookupServiceRepository lookupServiceRepository;
        public IPatientServiceRepository patientServiceRepository;
        public IAppointmentServiceRepository appointmentServiceRepository;
        public IUserServiceRepository userServiceRepository;
        public UserController(ICustomerServiceRepository _customerServiceRepository, ILookupServiceRepository _lookupServiceRepository,
            IPatientServiceRepository _patientServiceRepository, IAppointmentServiceRepository _appointmentServiceRepository,
            IUserServiceRepository _userServiceRepository)
        {
            customerServiceRepository = _customerServiceRepository;
            lookupServiceRepository = _lookupServiceRepository;
            patientServiceRepository = _patientServiceRepository;
            appointmentServiceRepository = _appointmentServiceRepository;
            userServiceRepository = _userServiceRepository;
        }
        public IActionResult MyDashboard()
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            int _userId = HttpContext.Session.GetInt32("UserId").Value;
            var result = appointmentServiceRepository.GetAllAppointments(_userId);
            ViewData["UserData"] = userServiceRepository.GetUser(_userId);
            return View(result);
        }
        public IActionResult BookAppointment()
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            return View();
        }
        public JsonResult GetSearchData(string searchText)
        {
            if (searchText == null) searchText = string.Empty;
            var customers = customerServiceRepository.GetCustomers().Where(_ => _.Area.ToUpper().Contains(searchText.ToUpper()) && _.Includesearch == 1 && _.Subscriber == 1)
                .Select(_ => new { _.Id, _.Name }).ToList();
            var clinics = customerServiceRepository.GetClinics().Where(_ => _.Area.ToUpper().Contains(searchText.ToUpper())).Select(_ => new { _.Id, _.Name }).ToList();
            List<Tuple<string, string, string>> result = new List<Tuple<string, string, string>>();
            foreach (var record in customers)
                result.Add(new Tuple<string, string, string>("Doctors", record.Id.ToString(), record.Name));
            foreach (var record in clinics)
                result.Add(new Tuple<string, string, string>("Clinics", record.Id.ToString(), record.Name));
            return new JsonResult(result);
        }
        [HttpPost]
        public IActionResult SearchResult(IFormCollection form)
        {
            ViewData["Username"] = HttpContext.Session.GetString("Username");
            string area = form["area"];
            int specilalistid = Convert.ToInt32(form["specialist"]);
            ViewData["Specialists"] = lookupServiceRepository.GetSpecialistTypes();
            ViewData["specialistid"] = specilalistid;
            var result = customerServiceRepository.SearchDoctors(area, specilalistid);
            ViewData["count"] = result.Count;
            return View(result);
        }
        public IActionResult AppointmentForm()
        {
            var arealist = customerServiceRepository.GetCustomers().Select(_ => _.Area).Distinct().ToList();
            ViewData["Area"] = arealist;
            ViewData["Specialists"] = lookupServiceRepository.GetSpecialistTypes();
            if(HttpContext.Session.GetInt32("UserId").HasValue)
            {
                int userid = HttpContext.Session.GetInt32("UserId").Value;
                ViewData["Patients"] = patientServiceRepository.GetPatients(userid);
            }
            AppointmentViewModel appointmentViewModel = new AppointmentViewModel();
            return PartialView("_Appointment", appointmentViewModel);
        }
        public IActionResult PatientList()
        {
            PatientViewModel viewModel = new PatientViewModel();
            return View(viewModel);
        }
        public IActionResult GetPatients()
        {
            int userid = HttpContext.Session.GetInt32("UserId").Value;
            var patients = patientServiceRepository.GetPatients(userid);
            return PartialView("_Patients", patients);
        }
        public JsonResult SavePatient(PatientViewModel model)
        {
            model.UserId = HttpContext.Session.GetInt32("UserId").Value;
            model.Status = 1;
            bool result = patientServiceRepository.AddUpdatePatient(model);
            return new JsonResult(result);
        }
        public JsonResult GetDoctors(string specialistid, string area)
        {
            var result = customerServiceRepository.GetCustomers().Where(_ => _.Area.Equals(area) && _.Specialistid == Convert.ToInt32(specialistid)).ToList();
            return new JsonResult(result);
        }
        public JsonResult GetCities()
        {
            var result = lookupServiceRepository.GetCities().Select(_ => _.Name).ToList();
            return new JsonResult(result);
        }
        [HttpPost]
        public IActionResult DoctorList(IFormCollection collection)
        {
            DoctorSearchListViewModel viewModel = new DoctorSearchListViewModel();
            viewModel.ClinicId = Convert.ToInt32(collection["searchid"]);
            viewModel.Doctors = customerServiceRepository.GetCustomersByClinic(viewModel.ClinicId);
            if (viewModel.Doctors != null)
                viewModel.DoctorCount = viewModel.Doctors.Count;
            viewModel.ClinicName = customerServiceRepository.GetClinic(viewModel.ClinicId).Name;
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult ShowTimings(IFormCollection collection)
        {
            ShowTimingsViewModel viewModel = new ShowTimingsViewModel()
            {
                DoctorId = Convert.ToInt32(collection["doctor"]),
                DoctorName = Convert.ToString(collection["doctorname"]),
                ClinicId = Convert.ToInt32(collection["clinic"]),
                ClinicName = Convert.ToString(collection["clinicname"]),
                doctorViewModel = customerServiceRepository.GetDoctor(Convert.ToInt32(collection["doctor"]))
            };
            return View(viewModel);
        }
        public IActionResult GetVisitorTimings(string customer, string clinic, string appointmentdate)
        {
            var data = new List<DaywiseTimingsViewModel>();
            int customerId = Convert.ToInt32(customer);
            int clinicId = Convert.ToInt32(clinic);
            data = customerServiceRepository.GetCustomerDaywiseTimingsData(customerId, clinicId, appointmentdate);
            return PartialView("_ShowTimings", data);
        }
        public IActionResult GetPatientsList()
        {
            int userid = HttpContext.Session.GetInt32("UserId").Value;
            ViewData["Patients"] = patientServiceRepository.GetPatients(userid);
            return PartialView("_ShowPatients");
        }
        public JsonResult SaveAppointment(AppointmentViewModel viewModel)
        {
            int userid = HttpContext.Session.GetInt32("UserId").Value;            
            var result = appointmentServiceRepository.AddAppointment(viewModel, userid, true);
            return Json(result);
        }
        [HttpPost]
        public IActionResult BookingResult(IFormCollection collection)
        {
            int bookingId = Convert.ToInt32(collection["bookingId"]);
            AppointmentViewModel viewModel = appointmentServiceRepository.GetAppointmentDetail(bookingId);
            return View(viewModel);
        }
    }
}
