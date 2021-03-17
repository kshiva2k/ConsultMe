using System;
using System.Collections.Generic;

namespace ConsultMe.Data.Models
{
    public partial class Clinic
    {
        public Clinic()
        {
            Appointment = new HashSet<Appointment>();
            Doctortimings = new HashSet<Doctortimings>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contactnumber { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Active { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual ICollection<Doctortimings> Doctortimings { get; set; }
    }
}
