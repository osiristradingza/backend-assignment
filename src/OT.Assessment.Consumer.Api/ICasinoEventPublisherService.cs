using OT.Assessment.Consumer.Api.Entities;

namespace OT.Assessment.Consumer.Api
{
    public interface ICasinoEventPublisherService
    {
        void PublishWagerEvent(CasinoWager wagerEvent);
    }
}
