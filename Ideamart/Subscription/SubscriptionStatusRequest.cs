using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideamart.Subscription
{
    public class SubscriptionStatusRequest
    {
        public string applicationId { get; set; }
        public string password { get; set; }
        public string subscriberId { get; set; }
    }
}