using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class AddGameRequest
    {
        [Required(ErrorMessage = "Game Name is required.")]
        [StringLength(100, ErrorMessage = "Game Name cannot exceed 100 characters.")]
        public string GameName { get; set; }

        [StringLength(500, ErrorMessage = "Game Description cannot exceed 500 characters.")]
        public string GameDescription { get; set; }

        [StringLength(100, ErrorMessage = "Theme cannot exceed 100 characters.")]
        public string Theme { get; set; }

        [Required(ErrorMessage = "Provider Name is required.")]
        [StringLength(100, ErrorMessage = "Provider Name cannot exceed 100 characters.")]
        public string ProviderName { get; set; }
    }
}
