using System;
using System.Collections.Generic;

namespace ConsultMe.Data.Models
{
    public partial class Doctortimings
    {
        public Doctortimings()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public int? Doctorid { get; set; }
        public int? Clinicid { get; set; }
        public decimal? Starttime { get; set; }
        public decimal? Endtime { get; set; }
        public int? Reviewtime { get; set; }
        public int? Bookinglimit { get; set; }

        public virtual Clinic Clinic { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
