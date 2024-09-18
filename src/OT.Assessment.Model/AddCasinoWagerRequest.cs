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
        [Required]
        public string Theme { get; set; }
        [Required]
        public string ProviderName { get; set; }
        [Required]
        public string GameName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string TransactionType { get; set; }
        [Required]
        [Range(1, 9999999999999999.99)]
        public decimal Amount { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int NumberOfBets { get; set; }

    }
}
