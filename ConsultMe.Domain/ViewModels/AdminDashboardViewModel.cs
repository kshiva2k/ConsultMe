using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ConsultMe.Domain.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int DoctorCount { get; set; }
        public int UserCount { get; set; }
    }
}
