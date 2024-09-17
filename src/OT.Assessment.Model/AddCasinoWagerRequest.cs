using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class AddCasinoWagerRequest
    {
        public string Theme { get; set; }
        public string ProviderName { get; set; }
        public string GameName { get; set; }
        public string Username { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string CountryCode { get; set; }
        public int NumberOfBets { get; set; }

    }
}
