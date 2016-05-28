using System.ComponentModel.DataAnnotations.Schema;

namespace MergeIdentity.Context
{
    [Table("ApplicationCalculationData")]
    public partial class ApplicationCalculationData
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public int SeServiceId { get; set; }

        public string EncryptedPhoneNumber { get; set; }

        public virtual Application Application { get; set; }
    }
}
