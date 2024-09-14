using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class PlayerWagers
    {
        public Guid WagerId { get; set; }
        public Guid? GameId { get; set; }
        public Guid? TransactionId { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? ExternalReferenceId { get; set; }
        public Guid? TransactionTypeId { get; set; }
        public Guid? SessionId { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfBets { get; set; }
        public string CountryCode { get; set; }
        public long Duration { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
