using System;

namespace MergeIdentity.Context
{
    public partial class ScoringResult
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public double? Value { get; set; }

        public int State { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; }

        public virtual Application Application { get; set; }
    }
}
