using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class ApplicationPlayersResponse
    {
        public Guid? AccountID { get; set; }
        public string? AccountNumber { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? PhysicalAddress1 { get; set; }
        public string? PhysicalAddress2 { get; set; }
        public string? PhysicalAddress3 { get; set; }
        public string? PhysicalCode { get; set; }
        public string? PostalAddress1 { get; set; }
        public string? PostalAddress2 { get; set; }
        public string? PostalAddress3 { get; set; }
        public string? PostalCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? Active { get; set; }
        public Guid? BrandId { get; set; }

    }
}
