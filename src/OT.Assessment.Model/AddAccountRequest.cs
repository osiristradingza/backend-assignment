using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class AddAccountRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [EmailAddress]
        [Required]
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
    }
}
