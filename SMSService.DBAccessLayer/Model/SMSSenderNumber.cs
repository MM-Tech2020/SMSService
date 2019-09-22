namespace SMSService.DBAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SMSSenderNumber
    {
        public int? MNumberId { get; set; }

        public int Id { get; set; }

        public int? SMSId { get; set; }

        public virtual MobileNumber MobileNumber { get; set; }

        public virtual OutGoingSMSBasicInfo OutGoingSMSBasicInfo { get; set; }
    }
}
