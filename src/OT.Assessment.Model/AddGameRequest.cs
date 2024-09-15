using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class AddGameRequest
    {
        public string GameName { get; set; }
        public string GameDescription { get; set; }
        public string Theme { get; set; }
        public string ProviderName { get; set; }
        
    }
}
