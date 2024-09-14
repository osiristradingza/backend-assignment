using OT.Assessment.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Database.Interface
{
    public interface IWages
    {
        Task<IEnumerable<Wager>> GetAllWagersAsync();

    }
}
