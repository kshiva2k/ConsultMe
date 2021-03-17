using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;
using ConsultMe.Domain.ViewModels;
using ConsultMe.Service.Repository;

namespace ConsultMe.Service.Services
{
    public class UserService : IUserServiceRepository
    {
        public IUserRepository userRepository;
        public ICustomerServiceRepository customerServiceRepository;
        public UserService(IUserRepository _userRepository, ICustomerServiceRepository _customerServiceRepository)
        {
            userRepository = _userRepository;
            customerServiceRepository = _customerServiceRepository;
        }
        public UserViewModel CheckAuthentication(LoginViewModel _loginViewModel)
        {
            UserViewModel data = new UserViewModel();
            var users = userRepository.GetUsers();
            var user = users.Where(_ => (_.Email.Equals(_loginViewModel.Username) || _.Mobilenumber.Equals(_loginViewModel.Username)) && _.Password.Equals(_loginViewModel.Password)
                    && _.Status == 1).FirstOrDefault();
            if (user != null && user.Id > 0)
            {
                data.Id = user.Id;
                data.Loginname = _loginViewModel.Username;
                data.Displayname = user.Displayname;
                data.Usertype = user.Usertypeid.GetValueOrDefault();
            }
            return data;
        }
        public bool AddUpdateUser(UserViewModel _userViewModel, int _userId)
        {
            try
            {
                if (_userViewModel.Status == 0)
                    userRepository.DeleteUser(_userViewModel.Id);
                else
                {
                    User user = new User()
                    {
                        Id = _userViewModel.Id,
                        Email = _userViewModel.Email,
                        Mobilenumber = _userViewModel.Mobilenumber,
                        Displayname = _userViewModel.Displayname,
                        Password = _userViewModel.Password,
                        Usertypeid = _userViewModel.Usertype,
                        Status = 1
                    };
                    if (_userViewModel.Id == 0)
                        userRepository.UpdateUser(user);
                    else
                        userRepository.AddUser(user);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<UserViewModel> GetUsers()
        {
            var data = new List<UserViewModel>();
            var lstusers = userRepository.GetUsers().Where(_ => _.Status == 1).ToList();
            foreach (var user in lstusers)
            {
                data.Add(new UserViewModel()
                {
                    Id = user.Id,
                    Displayname = user.Displayname,
                    Email = user.Email,
                    Mobilenumber = user.Mobilenumber
                });
            }
            return data;
        }
        public UserViewModel GetUser(int _userId)
        {
            UserViewModel data = new UserViewModel();
            var user = userRepository.GetUsers().Where(_ => _.Id.Equals(_userId)).FirstOrDefault();
            data.Id = user.Id;
            data.Displayname = user.Displayname;
            data.Email = user.Email;
            data.Mobilenumber = user.Mobilenumber;
            data.DoctorId = user.Doctorid;
            data.Usertype = user.Usertypeid.GetValueOrDefault();
            return data;
        }
        public AdminDashboardViewModel GetAdminDashboardData()
        {
            int customercount = customerServiceRepository.GetCustomers().Count;
            int usercount = userRepository.GetUsers().Count;
            AdminDashboardViewModel adminDashboardViewModel = new AdminDashboardViewModel()
            {
                DoctorCount = customercount,
                UserCount = usercount
            };
            return adminDashboardViewModel;
        }
    }
}
