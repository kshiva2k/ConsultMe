using System;

namespace ConsultMe.Domain.ViewModels
{
    public class AppointmentViewModel
    {
        public long Id { get; set; }
        public int CustomerId { get; set; }
        public string DoctorName { get; set; }
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public int Userid { get; set; }
        public string BookingNumber { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Symptoms { get; set; }
        public string Appointmentdate { get; set; }
        public string AppointmentType { get; set; }
        public string SlotTime { get; set; }
        public DateTime Createddate { get; set; }
        public string Status { get; set; }
        public string CurrentReviewNumber { get; set; }
    }
}
