using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class ReportPlayerWager
    {
        public Guid? WagerId { get; set; }
        public string? Game { get; set; }
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
