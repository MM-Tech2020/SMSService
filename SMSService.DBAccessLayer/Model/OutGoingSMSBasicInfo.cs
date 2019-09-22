namespace SMSService.DBAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OutGoingSMSBasicInfo")]
    public partial class OutGoingSMSBasicInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OutGoingSMSBasicInfo()
        {
            SMSSenderNumbers = new HashSet<SMSSenderNumber>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        [Required]
        [StringLength(15)]
        public string language { get; set; }

        public DateTime RequestDate { get; set; }

        public int? AppSenderId { get; set; }

        [StringLength(50)]
        public string DelayUntil { get; set; }

        public int? ResponseId { get; set; }

        public virtual ApplicationSender ApplicationSender { get; set; }

        public virtual Respons Respons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMSSenderNumber> SMSSenderNumbers { get; set; }
    }
}
