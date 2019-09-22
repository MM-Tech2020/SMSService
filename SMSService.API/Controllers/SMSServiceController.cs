using RestSharp;
using SMSService.API.Common;
using SMSService.API.Models;
using SMSService.DBAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SMSService.API.Controllers
{
    public class SMSServiceController : ApiController
    {
        private HttpResponseMessage Response = new HttpResponseMessage();
        

        // GET: api/SMSService
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SMSService/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SMSService

        public HttpResponseMessage Post([FromBody] SMSClientDTO SMSData)
        {
            if (!Utilities.ValidetAppAndSender(SMSData.AppId, SMSData.Sender))
            {
                return new HttpResponseMessage
                {
                    ReasonPhrase = "Application Code Or Sender Name not valid",
                    StatusCode = HttpStatusCode.BadRequest
                };

            }
            using (SMSContext db = new SMSContext())
            {
                try
                {
                    List<SMSSenderNumber> SenderNumbers = new List<SMSSenderNumber>();
                foreach (string number in SMSData.MobileNumbers)
                {
                    var id = db.MobileNumbers.Where(c => c.Number == number).Select(s => s.Id).FirstOrDefault();
                    if (id == 0)
                    {
                        var MNumber = db.MobileNumbers.Add(new MobileNumber() { Number = number });
                        db.SaveChanges();
                        SenderNumbers.Add(new SMSSenderNumber() { MNumberId = MNumber.Id });
                    }
                    else
                    {
                        SenderNumbers.Add(new SMSSenderNumber() { MNumberId = id });
                    }
                }
                OutGoingSMSBasicInfo SMSInfo = new OutGoingSMSBasicInfo()
                {
                    AppSenderId = db.ApplicationSenders.Where(c => c.SenderId == SMSData.Sender & c.AppId == SMSData.AppId).Select(s => s.Id).FirstOrDefault(),
                    DelayUntil = SMSData.DelayUntil.ToString(),
                    language = SMSData.Language.ToString(),
                    Message = SMSData.TextMessage,
                    RequestDate = DateTime.Now,
                    SMSSenderNumbers = SenderNumbers,

                };
                db.OutGoingSMSBasicInfoes.Add(SMSInfo);
                
                    db.SaveChanges();
          
               IRestResponse ResponseContent = Utilities.CallSMSServiceProvider(SMSData);
                SMSResponseDTO result = Newtonsoft.Json.JsonConvert.DeserializeObject<SMSResponseDTO>(ResponseContent.Content);
                    Respons Respons = new Respons()
                {
                    ResponseObject = ResponseContent.Content,
                    StatusId = db.Status.Where(c => c.StatusCode == result.code).Select(s => s.Id).FirstOrDefault(),

                };
                    db.Responses.Add(Respons);
                    db.SaveChanges();
                    SMSInfo.ResponseId = Respons.Id;
               //     db.OutGoingSMSBasicInfoes.Attach(SMSInfo);
                db.SaveChanges();
                return new HttpResponseMessage() {
                    ReasonPhrase = "Success",

                    StatusCode = HttpStatusCode.OK
                };
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage
                    {
                        ReasonPhrase = ex.Message,
                        
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }
            }
        }

        // PUT: api/SMSService/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SMSService/5
        public void Delete(int id)
        {
        }
    }
}
