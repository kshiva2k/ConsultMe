using System;
using System.Collections.Generic;

namespace ConsultMe.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Appointment = new HashSet<Appointment>();
            Customertimings = new HashSet<Customertimings>();
            Feedback = new HashSet<Feedback>();
            Subscriptions = new HashSet<Subscriptions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Specialization { get; set; }
        public int? Specialistid { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Ratings { get; set; }
        public int? Status { get; set; }

        public virtual Lookupspecialist Specialist { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Customertimings> Customertimings { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
        public virtual ICollection<Subscriptions> Subscriptions { get; set; }
    }
}
