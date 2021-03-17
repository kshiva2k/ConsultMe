using System;
namespace ConsultMe.Domain.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string RefNo { get; set; }
        public int Status { get; set; }
    }
}
