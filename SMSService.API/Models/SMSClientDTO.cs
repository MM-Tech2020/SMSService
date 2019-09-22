using System;
using System.Collections.Generic;

namespace SMSService.API.Models
{
    public class SMSClientDTO
    {
        public  Languages Language { get; set; }
        public string TextMessage  { get; set; }
        public int AppId  { get; set; }
        public DateTime DelayUntil  { get; set; }
        public string Sender { get; set; }
        public List<string> MobileNumbers { get; set; }
        public DateTime RealTime { get; set; }

    }
}