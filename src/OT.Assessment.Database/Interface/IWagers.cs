using OT.Assessment.Database.Tables;
using OT.Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Database.Interface
{
    public interface IWagers
    {
        Task<IEnumerable<PlayerWagers>> GetAllWagersAsync();
        Task<AddCasinoWagerResponse> PlayerWagerAsync(AddCasinoWagerRequest addCasinoWager);
    }
}
