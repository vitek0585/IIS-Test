using System.ComponentModel.DataAnnotations;

namespace LogOutUser.Models.Entity
{
    public partial class TelcoConnectionPhoneCode
    {
        public int Id { get; set; }

        public int TelcoConnectionId { get; set; }

        [Required]
        [StringLength(12)]
        public string Value { get; set; }

        public virtual TelcoConnection TelcoConnection { get; set; }
    }
}
