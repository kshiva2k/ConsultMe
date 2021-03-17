using ConsultMe.Data.Repository;
using System;
using System.Collections.Generic;
using ConsultMe.Domain.ViewModels;
using ConsultMe.Data.Models;
using ConsultMe.Service.Repository;

namespace ConsultMe.Service.Services
{
    public class SubscriptionService : ISubscriptionServiceRepository
    {
        public ISubscriptionRepository subscriptionRepository;
        public SubscriptionService(ISubscriptionRepository _subscriptionRepository)
        {
            subscriptionRepository = _subscriptionRepository;
        }
        public List<SubscriptionViewModel> GetSubscription(int _customerId)
        {
            var list = subscriptionRepository.GetSubscriptions(_customerId);
            var data = new List<SubscriptionViewModel>();
            foreach(Subscriptions subscription in list)
            {
                data.Add(new SubscriptionViewModel()
                {
                    Id = subscription.Id,
                    Startdate = subscription.Startdate.GetValueOrDefault(),
                    Enddate = subscription.Enddate.GetValueOrDefault()
                });
            }
            return data;
        }
        public bool AddSubscription(SubscriptionViewModel _viewModel)
        {
            try
            {
                Subscriptions subscription = new Subscriptions()
                {
                    Id = _viewModel.Id,
                    Startdate = _viewModel.Startdate,
                    Enddate = _viewModel.Enddate
                };
                subscriptionRepository.AddSubscription(subscription);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
