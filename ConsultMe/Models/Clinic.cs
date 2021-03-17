using System;
using System.Collections.Generic;

namespace ConsultMe.Models
{
    public partial class Clinic
    {
        public Clinic()
        {
            Customertimings = new HashSet<Customertimings>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contactnumber { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Active { get; set; }

        public virtual ICollection<Customertimings> Customertimings { get; set; }
    }
}
