using System;
namespace ConsultMe.Domain.ViewModels
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Specialization { get; set; }
        public int Specialistid { get; set; }
        public string Specialist { get; set; }
        public string Area { get; set; }
        public int City { get; set; }
        public string State { get; set; }
        public int Ratings { get; set; }
        public int Status { get; set; }
        public string MemberSince { get; set; }
        public byte Subscriber { get; set; }
        public byte Includesearch { get; set; }
    }
    public class CustomerDashboardViewModel
    {
        public int OverallPatientsCount { get; set; }
        public int TodayPatientCount { get; set; }
        public int TodayAppointments { get; set; }
    }
}
