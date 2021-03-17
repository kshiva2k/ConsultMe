using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultMe.Domain.ViewModels
{
    public class DaywiseTimingsViewModel
    {
        public string AppointmentDate { get; set; }
        public string AppointmentDay { get; set; }
        public List<DoctorTimingsViewModel> DoctorTimings { get; set; }
    }
}
