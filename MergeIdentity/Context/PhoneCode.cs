using System.ComponentModel.DataAnnotations;

namespace MergeIdentity.Context
{
    public partial class PhoneCode
    {
        public int Id { get; set; }

        public int TelcoConnectionId { get; set; }

        [Required]
        [StringLength(12)]
        public string Value { get; set; }

        public virtual TelcoConnection TelcoConnection { get; set; }
    }
}
