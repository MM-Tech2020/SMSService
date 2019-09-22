namespace SMSService.DBAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Status()
        {
            Responses = new HashSet<Respons>();
        }

        public int Id { get; set; }

        [Column("Status")]
        [Required]
        [StringLength(100)]
        public string Status1 { get; set; }

        [StringLength(10)]
        public string StatusCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Respons> Responses { get; set; }
    }
}
