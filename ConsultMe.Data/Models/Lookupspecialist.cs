using System;
using System.Collections.Generic;

namespace ConsultMe.Data.Models
{
    public partial class Lookupspecialist
    {
        public Lookupspecialist()
        {
            Doctor = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Doctor> Doctor { get; set; }
    }
}
