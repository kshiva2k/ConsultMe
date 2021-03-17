using System;
using System.Collections.Generic;

namespace ConsultMe.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int? Customerid { get; set; }
        public long? Appointmentid { get; set; }
        public string Comments { get; set; }
        public int? Ratings { get; set; }
        public int? Active { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
