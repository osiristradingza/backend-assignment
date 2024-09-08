using OT.Assessment.Consumer.Api;
using OT.Assessment.Consumer.Api.Entities;
using OT.Assessment.Consumer.Data;

namespace OT.Assessment.Consumer
{
    public class GameService : IGameService
    {
        private readonly IGameReader _gameReader;
        public GameService(IGameReader gameReader)
        {
            _gameReader = gameReader;
        }
        
        public Game GetGameByExternaReferenceId(Guid externaReferenceId)
        {
            return _gameReader.GetGameByExternaReferenceId(externaReferenceId); 
        }
    }
}
