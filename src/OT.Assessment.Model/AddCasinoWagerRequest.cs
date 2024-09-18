using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class AddCasinoWagerRequest
    {
        [Required(ErrorMessage = "Theme is required.")]
        [StringLength(100, ErrorMessage = "Theme cannot exceed 100 characters.")]
        public string Theme { get; set; }

        [Required(ErrorMessage = "Provider Name is required.")]
        [StringLength(100, ErrorMessage = "Provider Name cannot exceed 100 characters.")]
        public string ProviderName { get; set; }

        [Required(ErrorMessage = "Game Name is required.")]
        [StringLength(100, ErrorMessage = "Game Name cannot exceed 100 characters.")]
        public string GameName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Transaction Type is required.")]
        [StringLength(50, ErrorMessage = "Transaction Type cannot exceed 50 characters.")]
        public string TransactionType { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, 9999999999999999.99, ErrorMessage = "Amount must be between 1 and 9999999999999999.99.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Country Code is required.")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Country Code must be a valid ISO 3166-1 alpha-2 country code (e.g., US, GB).")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Number of Bets is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Bets must be at least 1.")]
        public int NumberOfBets { get; set; }
    }
}
