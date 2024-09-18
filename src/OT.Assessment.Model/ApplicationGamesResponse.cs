using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class ApplicationGamesResponse
    {
        public Guid? GameId { get; set; }
        public string? GameName { get; set; }
        public string? Description { get; set; }
        public string? Theme { get; set; }
        public Guid? ProviderID { get; set; }
        public string? ProviderName { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
