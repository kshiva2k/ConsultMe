using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultMe.Service.Enums
{
    public enum UserTypeEnum
    {
        Administrator = 1,
        CustomerAdmin = 2,
        Customer = 3,
        User = 4
    }
    public enum AppointmentStatusEnum
    {
        Confirmed = 1,
        UnderReview = 2,
        Completed = 3,
        Cancelled = 4
    }
    public enum AppointmentTypeEnum
    {
        Normal = 1,
        Emergency = 2
    }
}
