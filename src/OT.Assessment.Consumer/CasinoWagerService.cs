using Newtonsoft.Json.Linq;
using OT.Assessment.Consumer.Api;
using OT.Assessment.Consumer.Api.Entities;
using OT.Assessment.Consumer.Data;

namespace OT.Assessment.Consumer
{
    public class CasinoWagerService : ICasinoWagerService
    {
        private readonly ICasinoWagerWriter _casinoWagerWriter;
        private readonly ICasinoWagerReader _casinoWagerReader;
        private readonly ICasinoEventPublisherService _casinoEventPublisherService;
        public CasinoWagerService(ICasinoWagerWriter casinoWagerWriter, ICasinoWagerReader casinoWagerReader, ICasinoEventPublisherService casinoEventPublisherService)
        {
            _casinoWagerWriter = casinoWagerWriter;
            _casinoWagerReader = casinoWagerReader;
            _casinoEventPublisherService = casinoEventPublisherService;
        }
        public void SaveWager(CasinoWager wager)
        {
            _casinoWagerWriter.Save(wager);
        }
        public void PublishMessage(CasinoWager wager)
        {
            _casinoEventPublisherService.PublishWagerEvent(wager);
        }
        public JObject GetCasinoWagersByPlayerAccountId(Guid aPlayerAccountId)
        {
            return _casinoWagerReader.GetCasinoWagersByAccountId(aPlayerAccountId);
        }
        public List<CasinoWager> GetAll()
        {
            return _casinoWagerReader.GetAll();
        }
    }
}
