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
        [Required(ErrorMessage = "Country Code is required.")]
        [MaxLength(2, ErrorMessage = "Country Code cannot exceed 2 characters.")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Country Code must be a valid ISO 3166-1 alpha-2 country code (e.g., US, GB).")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Country Name is required.")]
        [StringLength(100, ErrorMessage = "Country Name cannot exceed 100 characters.")]
        public string CountryName { get; set; }
    }
}
