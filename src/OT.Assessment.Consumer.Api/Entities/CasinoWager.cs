using System.Text.Json.Serialization;

namespace OT.Assessment.Consumer.Api.Entities
{
    public class CasinoWager
    {
        [JsonPropertyName("wagerId")]
        public Guid WagerId { get; set; }

        [JsonPropertyName("theme")]
        public string Theme { get; set; }

        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("gameName")]
        public string GameName { get; set; }

        [JsonPropertyName("transactionId")]
        public Guid TransactionId { get; set; }

        [JsonPropertyName("brandId")]
        public Guid BrandId { get; set; }

        [JsonPropertyName("accountId")]
        public Guid AccountId { get; set; }

        [JsonPropertyName("Username")]
        public string Username { get; set; }

        [JsonPropertyName("externalReferenceId")]
        public Guid ExternalReferenceId { get; set; }

        [JsonPropertyName("transactionTypeId")]
        public Guid TransactionTypeId { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonPropertyName("numberOfBets")]
        public int NumberOfBets { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("sessionData")]
        public string SessionData { get; set; }

        [JsonPropertyName("Duration")]
        public long Duration { get; set; }

        [JsonPropertyName("wagerStatusId")] 
        public int WagerStatusId { get; set; }

        [JsonPropertyName("modifiedDateTime")]
        public DateTime ModifiedDateTime { get; set; }

        [JsonPropertyName("isResulted")]
        public bool IsResulted { get; set; }
    }
}
