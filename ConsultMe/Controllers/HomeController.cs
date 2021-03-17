using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Service.Enums;
using ConsultMe.Domain.ViewModels;
using ConsultMe.Service.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Google.Protobuf.Reflection;

namespace ConsultMe.Controllers
{
    public class HomeController : Controller
    {
        public IUserServiceRepository userServiceRepository;
        public ICustomerServiceRepository customerServiceRepository;
        public HomeController(IUserServiceRepository _userServiceRepository, ICustomerServiceRepository _customerServiceRepository)
        {
            userServiceRepository = _userServiceRepository;
            customerServiceRepository = _customerServiceRepository;
        }
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                Username = "1234567890"
            };
            return View(loginViewModel);
        }
        [HttpPost]
        public IActionResult Index(IFormCollection formCollection)
        {
            string actionName = string.Empty, controllerName = string.Empty;
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                Username = formCollection["Username"],
                Password = formCollection["Password"]
            };
            UserViewModel result = userServiceRepository.CheckAuthentication(loginViewModel);
            if (result != null && result.Id > 0)
            {
                // Valid user
                HttpContext.Session.SetInt32("UserId", result.Id);
                HttpContext.Session.SetString("Username", result.Displayname);
                if (result.Usertype.Equals((int)UserTypeEnum.Administrator))
                {
                    actionName = "AdminDashboard";
                    controllerName = "Admin";
                }
                else if (result.Usertype.Equals((int)UserTypeEnum.User))
                {
                    actionName = "MyDashboard";
                    controllerName = "User";
                }
                else if (result.Usertype.Equals((int)UserTypeEnum.CustomerAdmin))
                {
                    var customer = customerServiceRepository.GetCustomerByUser(result.Id);
                    HttpContext.Session.SetInt32("CustomerId", customer.Id);
                    actionName = "Dashboard";
                    controllerName = "Customer";
                }
                else if (result.Usertype.Equals((int)UserTypeEnum.Customer))
                {
                    var user = userServiceRepository.GetUser(result.Id);
                    HttpContext.Session.SetInt32("CustomerId", user.DoctorId.GetValueOrDefault());
                    actionName = "AttDashboard";
                    controllerName = "Customer";
                }
                return RedirectToAction(actionName, controllerName);
            }
            else
            {
                loginViewModel.Password = "";
                // Invalid user
                ViewData["Error"] = "Invalid login !";
                return View(loginViewModel);
            }
        }
    }
}
