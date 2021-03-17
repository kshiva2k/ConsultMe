using ConsultMe.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultMe.Service.Repository
{
    public interface IUserServiceRepository
    {
        UserViewModel CheckAuthentication(LoginViewModel _loginViewModel);
        bool AddUpdateUser(UserViewModel _userViewModel, int _userId);
        List<UserViewModel> GetUsers();
        UserViewModel GetUser(int _userId);
        AdminDashboardViewModel GetAdminDashboardData();
    }
}
