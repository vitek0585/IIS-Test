using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogOutUser.Models.Entity
{
    public partial class Feature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Feature()
        {
            Attributes = new HashSet<Attribute>();
        }

        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string ExtractorId { get; set; }

        public int ScoreCardId { get; set; }

        public bool IsDiscrete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attribute> Attributes { get; set; }

        public virtual ScoreCard ScoreCard { get; set; }
    }
}
