namespace SMSService.DBAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ApplicationSender
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApplicationSender()
        {
            OutGoingSMSBasicInfoes = new HashSet<OutGoingSMSBasicInfo>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int AppId { get; set; }

        [Required]
        [StringLength(20)]
        public string SenderId { get; set; }

        [StringLength(150)]
        public string ServiceProvider { get; set; }

        public virtual Application Application { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutGoingSMSBasicInfo> OutGoingSMSBasicInfoes { get; set; }
    }
}
