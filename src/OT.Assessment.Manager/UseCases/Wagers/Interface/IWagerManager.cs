using OT.Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Manager.UseCases.Wagers.Interface
{
    public interface IWagerManager
    {
        Task<IEnumerable<PlayerWagers>> GetPlayerWagersAsync();

        Task<AddCasinoWagerResponse> PlayerWagerAsync(AddCasinoWagerRequest addCasinoWager);

        Task<string> PlayerWagerAsync(AddCasinoWagerRequest addCasinoWager,  bool UseMassages = false);
        Task<PlayerWagesResponse> GetPlayerWagesAsync(Guid playerId, int page = 1, int pageSize = 10);
        Task<IEnumerable<ReportGetTopSpenders>> GetTopSpendersAsync(int count = 10);
    }
}
