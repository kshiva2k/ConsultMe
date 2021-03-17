using System;
namespace ConsultMe.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Loginname { get; set; }
        public string Email { get; set; }
        public string Mobilenumber { get; set; }
        public string Displayname { get; set; }
        public string Password { get; set; }
        public int Usertype { get; set; }
        public int? DoctorId { get; set; }
        public int Status { get; set; }
    }
}
