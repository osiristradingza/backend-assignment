using OT.Assessment.Consumer.Api;
using OT.Assessment.Consumer.Api.Entities;
using OT.Assessment.Consumer.Data;

namespace OT.Assessment.Consumer
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerWriter _playerWriter;
        private readonly IPlayerReader _playerReader;
        public PlayerService(IPlayerWriter playerWriter, IPlayerReader playerReader)
        {
            _playerWriter = playerWriter;
            _playerReader = playerReader;
        }
        public void SavePlayer(Player player)
        {
            _playerWriter.Save(player);
        }
        public List<TopSpenderPlayer> GetTopSpenders(int count)
        {
            return _playerReader.GetTopSpenders(count); 
        }
        public List<Player> GetAll()
        {
            return _playerReader.GetAll();
        }
    }
}
