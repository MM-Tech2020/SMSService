using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSService.API.Models
{
    public class SMSResponseDTO
    {

        public string code { get; set; }
        public string Language { get; set; }
        public string SMSID { get; set; }

    }
}