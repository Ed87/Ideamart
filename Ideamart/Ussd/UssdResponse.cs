using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideamart.Ussd
{
    public class UssdResponse
    {
        public string applicationId
        {
            get;
            set;
        }

        public string encoding
        {
            get;
            set;
        }

        public string message
        {
            get;
            set;
        }

        public string requestId
        {
            get;
            set;
        }

        public string sessionId
        {
            get;
            set;
        }

        public string sourceAddress
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

        public string vlrAddress
        {
            get;
            set;
        }
    }
}