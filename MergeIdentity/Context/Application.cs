using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MergeIdentity.Context
{
    public partial class Application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Application()
        {
            ApplicationCalculationDatas = new HashSet<ApplicationCalculationData>();
            ApplicationHistories = new HashSet<ApplicationHistory>();
            ScoringResults = new HashSet<ScoringResult>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string ApplicationNumber { get; set; }

        [StringLength(128)]
        public string CreatorId { get; set; }

        public int State { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationCalculationData> ApplicationCalculationDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationHistory> ApplicationHistories { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScoringResult> ScoringResults { get; set; }
    }
}
