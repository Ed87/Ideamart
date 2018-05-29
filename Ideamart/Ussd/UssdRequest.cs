using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideamart.Ussd
{
    public class UssdRequest
    {
        public string applicationId { get; set; }

        public string destinationAddress { get; set; }

        public string encoding { get; set; }

        public string message { get; set; }

        public string password
        {
            get;
            set;
        }

        public string sessionId
        {
            get;
            set;
        }

        public string ussdOperation
        {
            get;
            set;
        }

        public string version
        {
            get;
            set;
        }
    }
}