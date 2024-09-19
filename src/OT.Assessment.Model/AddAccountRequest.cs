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
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(50, ErrorMessage = "Surname cannot exceed 50 characters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        public string? Email { get; set; }

        [StringLength(6, ErrorMessage = "Gender can be at most 6 characters (e.g., Male/Female).")]
        public string? Gender { get; set; }

        [StringLength(100, ErrorMessage = "Physical Address 1 cannot exceed 100 characters.")]
        public string? PhysicalAddress1 { get; set; }

        [StringLength(100, ErrorMessage = "Physical Address 2 cannot exceed 100 characters.")]
        public string? PhysicalAddress2 { get; set; }

        [StringLength(100, ErrorMessage = "Physical Address 3 cannot exceed 100 characters.")]
        public string? PhysicalAddress3 { get; set; }

        [RegularExpression(@"^\d{4,10}$", ErrorMessage = "Physical Code must be a number between 4 and 10 digits.")]
        public string? PhysicalCode { get; set; }

        [StringLength(100, ErrorMessage = "Postal Address 1 cannot exceed 100 characters.")]
        public string? PostalAddress1 { get; set; }

        [StringLength(100, ErrorMessage = "Postal Address 2 cannot exceed 100 characters.")]
        public string? PostalAddress2 { get; set; }

        [StringLength(100, ErrorMessage = "Postal Address 3 cannot exceed 100 characters.")]
        public string? PostalAddress3 { get; set; }

        [RegularExpression(@"^\d{4,10}$", ErrorMessage = "Postal Code must be a number between 4 and 10 digits.")]
        public string? PostalCode { get; set; }
    }
}
