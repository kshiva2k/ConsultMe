using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
namespace ConsultMe.Data.Repository
{
    public interface IFeedbackRepository
    {
        public List<Feedback> GetFeedbacks(int _customerId);
        public bool AddFeedback(Feedback _feedback);
        public bool DeleteFeedback(int _id);
    }
}
