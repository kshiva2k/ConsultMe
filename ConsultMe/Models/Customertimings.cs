using System;
using System.Collections.Generic;

namespace ConsultMe.Models
{
    public partial class Customertimings
    {
        public int Id { get; set; }
        public int? Customerid { get; set; }
        public int? Clinicid { get; set; }
        public string Clinictimings { get; set; }
        public int? Reviewtime { get; set; }
        public int? Bookinglimit { get; set; }

        public virtual Clinic Clinic { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
