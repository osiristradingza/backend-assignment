using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class ApplicationCountriesResponse
    {
        public Guid? CountryID { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
