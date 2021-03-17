using System;
using System.Collections.Generic;
using ConsultMe.Data.Models;
namespace ConsultMe.Data.Repository
{
    public interface ISubscriptionRepository
    {
        public List<Subscriptions> GetSubscriptions(int _customerId);
        public bool AddSubscription(Subscriptions _subscriptions);
        public bool UpdateSubscription(int _id);
    }
}
