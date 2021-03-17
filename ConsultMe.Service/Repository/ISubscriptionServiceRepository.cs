using ConsultMe.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace ConsultMe.Service.Repository
{
    public interface ISubscriptionServiceRepository
    {
        List<SubscriptionViewModel> GetSubscription(int _customerId);
        bool AddSubscription(SubscriptionViewModel _viewModel);
    }
}
