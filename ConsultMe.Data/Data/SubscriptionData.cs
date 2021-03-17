using System;
using System.Collections.Generic;
using System.Linq;
using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;

namespace ConsultMe.Data.Data
{
    public class SubscriptionData : ISubscriptionRepository
    {
        consultmeContext context { get; }
        public SubscriptionData(consultmeContext _context)
        {
            context = _context;
        }
        public List<Subscriptions> GetSubscriptions(int _customerId)
        {
            var users = context.Subscriptions
                    .Where(_ => _.Active == 1 && _.Doctorid == _customerId)
                    .OrderByDescending(_ => _.Id)
                    .ToList();
            return users;
        }
        public bool AddSubscription(Subscriptions _subscriptions)
        {
            var subscription = new Subscriptions
            {
                Doctorid = _subscriptions.Doctorid,
                Startdate = _subscriptions.Startdate,
                Enddate = _subscriptions.Enddate
            };
            context.Subscriptions.Add(subscription);
            context.SaveChanges();
            return true;
        }
        public bool UpdateSubscription(int _id)
        {
            var subscription = context.Subscriptions.Where(_ => _.Id.Equals(_id)).FirstOrDefault();
            subscription.Active = 0;
            context.SaveChanges();
            return true;
        }
    }
}
