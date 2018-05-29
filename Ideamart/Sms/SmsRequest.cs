using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideamart.Sms
{
    public class SmsRequest
    {
        public string message { get; set; }
        public List<string> destinationAddresses { get; set; }
        public string password { get; set; }
        public string applicationId { get; set; }
    }
}