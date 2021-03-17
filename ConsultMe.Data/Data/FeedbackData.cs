using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;

namespace ConsultMe.Data.Data
{
    public class FeedbackData : IFeedbackRepository
    {
        consultmeContext context { get; }
        public FeedbackData(consultmeContext _context)
        {
            context = _context;
        }
        public List<Feedback> GetFeedbacks(int _customerId)
        {
            var users = context.Feedback
                    .Where(_ => _.Active == 1 && _.Doctorid == _customerId)
                    .OrderByDescending(_ => _.Id)
                    .ToList();
            return users;
        }
        public bool AddFeedback(Feedback _feedback)
        {
            var feedback = new Feedback
            {
                Appointmentid = _feedback.Appointmentid,
                Doctorid = _feedback.Doctorid,
                Comments = _feedback.Comments,
                Ratings = _feedback.Ratings,
                Active = 1,
            };
            context.Feedback.Add(feedback);
            context.SaveChanges();
            return true;
        }
        public bool DeleteFeedback(int _id)
        {
            var feedback = context.Feedback.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            feedback.Active = 0;
            context.SaveChanges();
            return true;
        }
    }
}
