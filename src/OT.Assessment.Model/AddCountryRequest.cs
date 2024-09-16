using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class AddCountryRequest
    {
        [Required]
        [MaxLength(2)]
        public string CountryCode { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}
