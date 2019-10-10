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
    public  class Utilities
    {
       
        public static string ValidetAppAndSender(int AppId, string Sender)
        {
            using (SMSContext db = new SMSContext())
            {
                string result = db.ApplicationSenders.Where(c => c.AppId == AppId && c.SenderId == Sender).Select(s=>s.FunctionCall).FirstOrDefault();
                return result;
            }

        }
        public static IRestResponse CallSMSMisrProvider(SMSClientDTO SMSClient)
        {
               Configuration configuration = GetConfiguration(SMSClient.AppId, SMSClient.Sender);
            string FullURL = configuration.Domain + configuration.URI + "UserName=" + configuration.UserName + "&Password=" + configuration.Password + "&SMSLang=E&SMSSender=" + SMSClient.Sender +
            "&SMSReceiver=" + String.Concat(string.Join(",", SMSClient.MobileNumbers)) + "&SMSText=" + SMSClient.TextMessage;
         //   + "&DelayUntil=" + SMSClient.DelayUntil.Year + "-"+ SMSClient.DelayUntil.Month + "-" + SMSClient.DelayUntil.Day + "-" + SMSClient.DelayUntil.Hour + "-" + SMSClient.DelayUntil.Minute;

            RestClient client = new RestClient();


            var request = new RestRequest(FullURL, Method.GET);


            IRestResponse response = client.Execute(request);

            return response;
        }

        public static IRestResponse CallSMSVasProvider(SMSClientDTO SMSClient)
        {

            Configuration configuration = GetConfiguration(SMSClient.AppId, SMSClient.Sender);
            string FullURL = configuration.Domain + configuration.URI + "username=" + configuration.UserName + "&password=" + configuration.Password + "&language=" + SMSClient.Language.GetHashCode() + "&sender=" + SMSClient.Sender +
            "&mobile=" + String.Concat(string.Join(",", SMSClient.MobileNumbers)) + "&message=" + SMSClient.TextMessage;
            //   + "&DelayUntil=" + SMSClient.DelayUntil.Year + "-"+ SMSClient.DelayUntil.Month + "-" + SMSClient.DelayUntil.Day + "-" + SMSClient.DelayUntil.Hour + "-" + SMSClient.DelayUntil.Minute;

            RestClient client = new RestClient();


            var request = new RestRequest(FullURL, Method.POST);


            IRestResponse response = client.Execute(request);

            return response;
        }

        public static Configuration GetConfiguration(int AppId, string Sender)
        {
            //string directory = System.AppDomain.CurrentDomain.BaseDirectory;
            //string path = Path.Combine(directory, "Common\\ConfigSMS.json") ;

            using (SMSContext db = new SMSContext())
            {

                Configuration Property = db.ApplicationSenders.Where(c => c.AppId == AppId && c.SenderId == Sender).Select(
                    s => new Configuration
                    {
                        Domain = s.Provider.Domain,
                        URI = s.Provider.URI,
                        UserName = s.Provider.UserName,
                        Password = s.Provider.Password
                    }).FirstOrDefault();

                return Property;
            }
        }
    }
}
