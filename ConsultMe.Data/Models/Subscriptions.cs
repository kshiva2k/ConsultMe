using System;
using System.Collections.Generic;

namespace ConsultMe.Data.Models
{
    public partial class Subscriptions
    {
        public int Id { get; set; }
        public int? Doctorid { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int? Active { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
