using System;
using System.Collections.Generic;
namespace ConsultMe.Domain.ViewModels
{
    public class ShowTimingsViewModel
    {
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DoctorViewModel doctorViewModel { get; set; }
        public List<DoctorTimingsViewModel> timingsViewModel { get; set; }
    }
}
