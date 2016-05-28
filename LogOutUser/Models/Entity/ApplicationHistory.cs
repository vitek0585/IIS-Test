using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogOutUser.Models.Entity
{
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
