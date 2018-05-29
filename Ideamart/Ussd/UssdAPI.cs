using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Ideamart.Ussd
{
    public class UssdAPI
    {
        private string AppEncoding
        {
            get;
            set;
        }

        private string AppId
        {
            get;
            set;
        }

        private string AppPassword
        {
            get;
            set;
        }

        private string AppVersion
        {
            get;
            set;
        }

        public bool IsInProduction
        {
            get;
            set;
        }

        public UssdAPI(string appId, string appPassword)
        {
            this.AppId = appId;
            this.AppPassword = appPassword;
            this.AppVersion = "1.0";
            this.AppEncoding = "440";
        }

        public UssdAPI(string appId, string appPassword, string appVersion, string appEncoding)
        {
            this.AppId = appId;
            this.AppPassword = appPassword;
            this.AppVersion = appVersion;
            this.AppEncoding = appEncoding;
        }

        private string GetUssdServerUrl(bool isInProduction)
        {
            string str;
            try
            {
                string url = string.Empty;
                url = (isInProduction ? "https://api.dialog.lk/ussd/send" : "http://localhost:7000/ussd/send");
                str = url;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        public async Task<UssdStatusReponse> SendRequestAsync(string message, string sessionId, UssdOperation ussdOperation, string tel)
        {
            UssdRequest ideaMartRequest = new UssdRequest()
            {
                applicationId = this.AppId,
                destinationAddress = tel,
                encoding = this.AppEncoding,
                message = message,
                password = this.AppPassword,
                sessionId = sessionId,
                version = this.AppVersion
            };

            string ussdOperationType = string.Empty;
            switch (ussdOperation)
            {
                case UssdOperation.mt_init:
                    {
                        ussdOperationType = "mt-init";
                        break;
                    }
                case UssdOperation.mt_cont:
                    {
                        ussdOperationType = "mt-cont";
                        break;
                    }
                case UssdOperation.mt_fin:
                    {
                        ussdOperationType = "mt-fin";
                        break;
                    }
            }
            ideaMartRequest.ussdOperation = ussdOperationType;

            Uri uri = new Uri(GetUssdServerUrl(IsInProduction));
            using (var httpclient = new HttpClient())
            {
                httpclient.DefaultRequestHeaders.Accept.Clear();
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpclient.DefaultRequestHeaders.Host = uri.Host;
                StringContent content = new StringContent(JsonConvert.SerializeObject(ideaMartRequest));
                HttpContent httpcontent = content;
                httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpclient.PostAsync(uri, httpcontent); //.result<of T>

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UssdStatusReponse>(result);
                }

                var error = new UssdStatusReponse()
                {
                    statusDetail = "ERROR_API"
                };

                string errorStr = JsonConvert.SerializeObject(error);
                return JsonConvert.DeserializeObject<UssdStatusReponse>(errorStr);
            }
        }

        public enum UssdOperation
        {
            mt_init,
            mt_cont,
            mt_fin
        }
    }
}