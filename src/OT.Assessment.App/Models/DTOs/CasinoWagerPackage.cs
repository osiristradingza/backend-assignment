using System.ComponentModel.DataAnnotations;

namespace OT.Assessment.App.Models.DTOs
{
    public class CasinoWagerPackage
    {
        [Required]
        public Guid wagerId { get; set; }

        [Required]
        public string provider { get; set; }

        [Required]
        public string gameName { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double amount { get; set; }  // Changed to double

        [Required]
        public DateTime createdDateTime { get; set; }

        public string theme { get; set; } 

        public int numberOfBets { get; set; }  

        public string countryCode { get; set; }

        public string sessionData { get; set; }

        public long duration { get; set; }
    }
}
