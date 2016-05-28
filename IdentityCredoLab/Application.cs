namespace IdentityCredoLab
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Application()
        {
            ApplicationHistories = new HashSet<ApplicationHistory>();
        }

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(128)]
        public string CreatorId { get; set; }

        public int? ScoringIndex { get; set; }

        [StringLength(50)]
        public string ApplicationNumber { get; set; }

        public int State { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationHistory> ApplicationHistories { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
