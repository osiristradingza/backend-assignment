using OT.Assessment.Consumer.Api.Entities;

namespace OT.Assessment.Consumer.Api
{
    public interface IPlayerService
    {
        void SavePlayer(Player player);
        List<Player> GetAll();
        List<TopSpenderPlayer> GetTopSpenders(int count);
    }
}
