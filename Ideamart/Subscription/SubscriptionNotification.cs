using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideamart.Subscription
{
    public class SubscriptionNotification
    {
        public string applicationId { get; set; }
        public string frequency { get; set; }
        public string status { get; set; }
        public string subscriberId { get; set; }
        public string version { get; set; }
        public string timeStamp { get; set; }
    }
}