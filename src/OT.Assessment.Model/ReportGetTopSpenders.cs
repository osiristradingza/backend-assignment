using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class ReportGetTopSpenders
    {
        public Guid? AccountId { get; set; }
        public string Username { get; set; }
        public decimal? TotalAmountSpent { get; set; }
    }
}
