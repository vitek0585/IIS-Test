namespace IdentityCredoLab
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ApplicationHistory")]
    public partial class ApplicationHistory
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public DateTime Date { get; set; }

        public int Action { get; set; }

        public virtual Application Application { get; set; }
    }
}
