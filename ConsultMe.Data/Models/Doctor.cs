using System;
using System.Collections.Generic;

namespace ConsultMe.Data.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointment = new HashSet<Appointment>();
            Doctortimings = new HashSet<Doctortimings>();
            Feedback = new HashSet<Feedback>();
            Subscriptions = new HashSet<Subscriptions>();
            UserNavigation = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Specialization { get; set; }
        public int? Specialistid { get; set; }
        public string Area { get; set; }
        public int? Cityid { get; set; }
        public string State { get; set; }
        public int? Ratings { get; set; }
        public int? Status { get; set; }
        public short? Subscriber { get; set; }
        public short? Includesearch { get; set; }
        public int? Userid { get; set; }

        public virtual City City { get; set; }
        public virtual Lookupspecialist Specialist { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Doctortimings> Doctortimings { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Subscriptions> Subscriptions { get; set; }
        public virtual ICollection<User> UserNavigation { get; set; }
    }
}
