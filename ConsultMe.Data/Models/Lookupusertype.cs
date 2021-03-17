using System;
using System.Collections.Generic;

namespace ConsultMe.Data.Models
{
    public partial class Lookupusertype
    {
        public Lookupusertype()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
