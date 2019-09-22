namespace SMSService.DBAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Responses")]
    public partial class Respons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Respons()
        {
            OutGoingSMSBasicInfoes = new HashSet<OutGoingSMSBasicInfo>();
        }

        public int Id { get; set; }

        public string ResponseObject { get; set; }

        public int? StatusId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutGoingSMSBasicInfo> OutGoingSMSBasicInfoes { get; set; }

        public virtual Status Status { get; set; }
    }
}
