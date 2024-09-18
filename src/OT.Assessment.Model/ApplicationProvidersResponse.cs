using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class ApplicationProvidersResponse
    {
        public Guid? ProviderID { get; set; }
        public string? Name { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
