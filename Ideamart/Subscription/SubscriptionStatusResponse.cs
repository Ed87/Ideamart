using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideamart.Subscription
{
    public class SubscriptionStatusResponse
    {
        public string version { get; set; }
        public string subscriptionStatus { get; set; }
        public string statusCode { get; set; }
        public string statusDetail { get; set; }
    }
}