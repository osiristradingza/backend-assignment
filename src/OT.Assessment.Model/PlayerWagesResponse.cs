using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Model
{
    public class PlayerWagesResponse
    {
        public List<ReportPlayerWager?> data { get; set; }

        public int? page { get; set; }
        public int? pageSize { get; set; }
        public int? total { get; set; }
        public decimal? totalPages { get; set; }


    }
}
