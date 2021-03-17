using System;
using System.Collections.Generic;

namespace ConsultMe.Models
{
    public partial class Lookupspecialist
    {
        public Lookupspecialist()
        {
            Customer = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
    }
}
