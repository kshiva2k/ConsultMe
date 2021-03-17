using System;
using System.Collections.Generic;

namespace ConsultMe.Models
{
    public partial class Patient
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public int? Active { get; set; }

        public virtual User User { get; set; }
    }
}
