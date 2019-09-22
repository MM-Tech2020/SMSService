using Newtonsoft.Json;
using RestSharp;
using SMSService.API.Models;
using SMSService.DBAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SMSService.API.Common
{
    public static class Utilities
    {

        public static bool ValidetAppAndSender(int AppId, string Sender)
        {
            using (SMSContext db = new SMSContext())
            {
                var result = db.ApplicationSenders.Any(c => c.AppId == AppId && c.SenderId == Sender);
                return result;
            }

        }
        public static IRestResponse CallSMSServiceProvider(SMSClientDTO SMSClient)
        {

            Configuration configuration = LoadJson();
            string FullURL = configuration.Domain + configuration.URI + "username=" + configuration.UserName + "&password=" + configuration.Password + "&language=" + SMSClient.Language.GetHashCode() + "&sender=" + SMSClient.Sender +
            "&mobile=" + String.Concat(string.Join(",", SMSClient.MobileNumbers)) + "&message=" + SMSClient.TextMessage;
            //   + "&DelayUntil=" + SMSClient.DelayUntil.Year + "-"+ SMSClient.DelayUntil.Month + "-" + SMSClient.DelayUntil.Day + "-" + SMSClient.DelayUntil.Hour + "-" + SMSClient.DelayUntil.Minute;

            RestClient client = new RestClient();


            var request = new RestRequest(FullURL, Method.POST);


            IRestResponse response = client.Execute(request);

            return response;
        }
        public static Configuration LoadJson()
        {
            string directory = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(directory, "Common\\ConfigSMS.json");
            using (StreamReader file = new StreamReader(path))
            {
                string json = file.ReadToEnd();
                Configuration Property = JsonConvert.DeserializeObject<Configuration>(json);

                return Property;
            }
        }
    }
}