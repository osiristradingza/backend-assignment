using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class AddProviderRequest
    {
        [Required(ErrorMessage = "Provider Name is required.")]
        [StringLength(100, ErrorMessage = "Provider Name cannot exceed 100 characters.")]
        public string ProviderName { get; set; }
    }
}
