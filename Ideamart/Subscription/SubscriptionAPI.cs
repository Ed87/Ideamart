using Newtonsoft.Json;
using PerfectMom.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Ideamart.Subscription
{
    public class SubscriptionAPI
    {
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


        public bool IsInProduction
        {
            get;
            set;
        }

        public SubscriptionAPI(string appId, string appPassword)
        {
            this.AppId = appId;
            this.AppPassword = appPassword;
        }

        private string GetSubscriptionStatusQueryUrl(bool isInProduction)
        {
            string str;
            try
            {
                string url = string.Empty;
                url = (isInProduction ? "https://api.dialog.lk/subscription/getStatus" : "http://localhost:7000/subscription/getStatus");
                str = url;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        private string GetSubscriptionRequestUrl(bool isInProduction)
        {
            string str;
            try
            {
                string url = string.Empty;
                url = (isInProduction ? "https://api.dialog.lk/subscription/send" : "http://localhost:7000/subscription/send");
                str = url;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        public async Task<SubscriptionStatusResponse> RequestSubscriptionStatusAsync(string telephone)
        {
            SubscriptionStatusRequest ideaMartRequest = new SubscriptionStatusRequest()
            {
                applicationId = AppId,
                password = AppPassword,
                subscriberId = telephone,
            };


            Uri uri = new Uri(GetSubscriptionStatusQueryUrl(IsInProduction));

            using (var httpclient = new HttpClient())
            {
                httpclient.DefaultRequestHeaders.Accept.Clear();
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpclient.DefaultRequestHeaders.Host = uri.Host;
                StringContent content = new StringContent(JsonConvert.SerializeObject(ideaMartRequest));
                HttpContent httpcontent = content;
                httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpclient.PostAsync(uri, httpcontent);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SubscriptionStatusResponse>(result);
                }

                var error = new SubscriptionStatusResponse()
                {
                    statusDetail = "ERROR_API"
                };

                string errorStr = JsonConvert.SerializeObject(error);
                return JsonConvert.DeserializeObject<SubscriptionStatusResponse>(errorStr);
            } 
        }

        public enum SubscriptionAction
        {
            Unsubscribe = 0,
            Subscribe = 1
        }

        public async Task<QuerySubscriptionResult> QuerySubscriberBaseAsync()
        {
            QuerySubscription querySubscription = new QuerySubscription()
            {
                applicationId = this.AppId,
                password = this.AppPassword
            };

            Uri uri = new Uri("https://api.dialog.lk/subscription/query-base");
            using (var httpclient = new HttpClient())
            {

                httpclient.DefaultRequestHeaders.Accept.Clear();
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpclient.DefaultRequestHeaders.Host = uri.Host;
                StringContent content = new StringContent(JsonConvert.SerializeObject(querySubscription));
                HttpContent httpcontent = content;
                httpcontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpclient.PostAsync(uri, httpcontent);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<QuerySubscriptionResult>(result);
                }
            }
            return null;
        }

        public async Task<SubscriptionResponse> SendSubscriptionRequestAsync(string telephone, SubscriptionAction action)
        {
            string strAction = null;

            if (action == SubscriptionAction.Subscribe)
            {
                strAction = "1";
            }
            else
            {
                strAction = "0";
            }
            SubscriptionRequest ideaMartRequest = new SubscriptionRequest()
            {
                applicationId = AppId,
                password = AppPassword,
                subscriberId = telephone,
                action = strAction,
                // version = "1.0"
            };

            Uri uri = new Uri(GetSubscriptionRequestUrl(IsInProduction));
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
                    return JsonConvert.DeserializeObject<SubscriptionResponse>(result);
                }
                var error = new SubscriptionStatusResponse()
                {
                    statusDetail = "ERROR_API"
                };
                string errorStr = JsonConvert.SerializeObject(error);
                return JsonConvert.DeserializeObject<SubscriptionResponse>(errorStr);
            }
        }
    }
}