using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OT.Assessment.App.Models.Casino
{
    [Table("CasinoWagers")]
    public class CasinoWagers
    {
        [Key]
        [Column("WagerId")]
        public Guid WagerId { get; set; }

        [Column("Theme")]
        public string Theme { get; set; }

        [Column("Provider")]
        public string Provider { get; set; }

        [Column("GameName")]
        public string GameName { get; set;}

        [Column("TransactionId")]
        public Guid TransactionId { get; set;}

        [Column("BrandId")]
        public Guid BrandId { get; set; }

        [Column("AccountId")]
        public Guid AccountId { get; set;}

        [Column("Username")]
        public string Username { get; set;}

        [Column("ExternalReferenceId")]
        public Guid ExternalReferenceId { get; set; }

        [Column("TransactionTypeId")]
        public Guid TransactionTypeId { get; set; }

        [Column("Amount")]
        public decimal Amount { get; set;}

        [Column("CreatedDateTime")]
        public DateTime CreatedDateTime { get; set;}

        [Column("NumberOfBets")]
        public int NumberOfBets { get; set; }

        [Column("CountryCode")]
        public string CountryCode { get; set; }

        [Column("SessionData")]
        public string SessionData { get; set; }

        [Column("Duration")]
        public long Duration { get; set; }

        [Column("PlayerId")]
        public Guid PlayerId { get; set; }
    }
}
