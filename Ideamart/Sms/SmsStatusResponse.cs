using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideamart.Sms
{
    public class SmsStatusResponse
    {
        public string statusCode { get; set; }
        public string statusDetail { get; set; }
        public string requestId { get; set; }
        public string version { get; set; }
    }
}