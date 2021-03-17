using System;
namespace ConsultMe.Domain.ViewModels
{
    public class DoctorTimingsViewModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string CustomerName { get; set; }
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int ReviewTime { get; set; }
        public int BookingLimit { get; set; }        
    }
}
