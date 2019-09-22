namespace SMSService.DBAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InCommingSMS")]
    public partial class InCommingSM
    {
        public int Id { get; set; }

        public DateTime ResponseDateTime { get; set; }

        public int MNumberId { get; set; }

        [StringLength(1000)]
        public string MessageDesc { get; set; }

        public virtual MobileNumber MobileNumber { get; set; }
    }
}
