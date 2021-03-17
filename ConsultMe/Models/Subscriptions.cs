using System;
using System.Collections.Generic;

namespace ConsultMe.Models
{
    public partial class Subscriptions
    {
        public int Id { get; set; }
        public int? Customerid { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int? Active { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
