using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogOutUser.Models.Entity
{
    public partial class TelcoConnection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TelcoConnection()
        {
            TelcoConnectionPhoneCodes = new HashSet<TelcoConnectionPhoneCode>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Url { get; set; }

        [Required]
        [StringLength(500)]
        public string TelcoLogin { get; set; }

        [Required]
        [StringLength(1000)]
        public string TelcoPassword { get; set; }

        [Required]
        public string CertValue { get; set; }

        [Required]
        [StringLength(100)]
        public string CertPublicKeyExponent { get; set; }

        [Required]
        [StringLength(1000)]
        public string CertPublicKeyModulus { get; set; }

        public int CertKeySize { get; set; }

        public bool IsActive { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TelcoConnectionPhoneCode> TelcoConnectionPhoneCodes { get; set; }
    }
}
