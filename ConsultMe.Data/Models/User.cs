using System;
using System.Collections.Generic;

namespace ConsultMe.Data.Models
{
    public partial class User
    {
        public User()
        {
            Appointment = new HashSet<Appointment>();
            Doctor = new HashSet<Doctor>();
            Patient = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Mobilenumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Displayname { get; set; }
        public int? Usertypeid { get; set; }
        public int? Doctorid { get; set; }
        public int? Status { get; set; }

        public virtual Doctor DoctorNavigation { get; set; }
        public virtual Lookupusertype Usertype { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Doctor> Doctor { get; set; }
        public virtual ICollection<Patient> Patient { get; set; }
    }
}
