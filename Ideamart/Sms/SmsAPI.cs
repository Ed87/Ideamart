using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Ideamart.Sms
{
    public class SmsAPI
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

        public SmsAPI(string appId, string appPassword)
        {
            this.AppId = appId;
            this.AppPassword = appPassword;
        }

        private string GetSmsServerUrl(bool isInProduction)
        {
            string str;
            try
            {
                string url = (isInProduction ? "https://api.dialog.lk/sms/send" : "http://localhost:7000/sms/send");
                str = url;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        public async Task<SmsStatusResponse> SendSmsAsync(string message, List<string> addresses)
        {
            SmsRequest ideaMartRequest = new SmsRequest()
            {
                applicationId = AppId,
                password = AppPassword,
                message = message,
                destinationAddresses = addresses
            };

            Uri uri = new Uri(GetSmsServerUrl(IsInProduction));

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
                    return JsonConvert.DeserializeObject<SmsStatusResponse>(result);
                }

                //if we reach this far, something bad happned
                //To-DO
                //more smoother exception handling 
                var error = new SmsStatusResponse()
                {
                    statusDetail = "ERROR_API"
                };
                string errorStr = JsonConvert.SerializeObject(error);
                return JsonConvert.DeserializeObject<SmsStatusResponse>(errorStr);
            }
        }
    }
}