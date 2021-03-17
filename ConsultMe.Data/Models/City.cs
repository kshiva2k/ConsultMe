using System;
using System.Collections.Generic;

namespace ConsultMe.Data.Models
{
    public partial class City
    {
        public City()
        {
            Doctor = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public short? Status { get; set; }

        public virtual ICollection<Doctor> Doctor { get; set; }
    }
}
