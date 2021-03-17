using System;
using System.Collections.Generic;

namespace ConsultMe.Data.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Feedback = new HashSet<Feedback>();
        }

        public long Id { get; set; }
        public int Doctorid { get; set; }
        public int? Clinicid { get; set; }
        public int Timingid { get; set; }
        public int Userid { get; set; }
        public int Patientid { get; set; }
        public string Symptoms { get; set; }
        public int? Appointmenttype { get; set; }
        public DateTime Appointmentdate { get; set; }
        public string Bookingnumber { get; set; }
        public DateTime? Createddate { get; set; }
        public int? Active { get; set; }

        public virtual Clinic Clinic { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Doctortimings Timing { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
