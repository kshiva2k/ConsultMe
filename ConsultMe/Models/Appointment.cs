using System;
using System.Collections.Generic;

namespace ConsultMe.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Feedback = new HashSet<Feedback>();
        }

        public long Id { get; set; }
        public int? Customerid { get; set; }
        public int? Userid { get; set; }
        public int? Patientid { get; set; }
        public string Symptoms { get; set; }
        public DateTime? Appointmentdate { get; set; }
        public int? Bookingnumber { get; set; }
        public DateTime? Createddate { get; set; }
        public int? Active { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
