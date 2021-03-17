using System;
namespace ConsultMe.Domain.ViewModels
{
    public class ClinicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Contactnumber { get; set; }
        public int Status { get; set; }
    }
}
