using Newtonsoft.Json.Linq;
using OT.Assessment.Consumer.Api.Entities;

namespace OT.Assessment.Consumer.Api
{
    public interface ICasinoWagerService
    {
        void SaveWager(CasinoWager wager);
        void PublishMessage(CasinoWager wager);
        List<CasinoWager> GetAll();
        JObject GetCasinoWagersByPlayerAccountId(Guid aPlayerAccountId);
    }
}
