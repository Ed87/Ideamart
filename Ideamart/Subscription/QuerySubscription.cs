using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectMom.Subscription
{
    public class QuerySubscriptionResult
    {
        public string version { get; set; }
        public string baseSize { get; set;}
        public string statusCode { get; set; }
        public string statusDetail { get; set; }

    }

    public class QuerySubscription
    {
        public string applicationId { get; set; }
        public string password { get; set; }

    }
}