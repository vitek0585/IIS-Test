using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MergeIdentity.Context
{
    public partial class ScoreCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScoreCard()
        {
            Features = new HashSet<Feature>();
        }

        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int DataSourceType { get; set; }

        public double Importance { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int Version { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feature> Features { get; set; }
    }
}
