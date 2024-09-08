using OT.Assessment.Consumer.Api.Entities;

namespace OT.Assessment.Consumer.Api
{
    public interface IGameService
    {
        Game GetGameByExternaReferenceId(Guid externaReferenceId);
    }
}
