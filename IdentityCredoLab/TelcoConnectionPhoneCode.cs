namespace IdentityCredoLab
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
