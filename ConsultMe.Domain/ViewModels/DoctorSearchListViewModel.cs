using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultMe.Domain.ViewModels
{
    public class DoctorSearchListViewModel
    {
        public int DoctorCount { get; set; }
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public List<DoctorViewModel> Doctors { get; set; }
    }
}
