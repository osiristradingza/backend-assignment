using Dapper;
using Microsoft.Extensions.Configuration;
using OT.Assessment.Consumer.Api.Entities;
using System.Data;
using System.Data.SqlClient;

namespace OT.Assessment.Consumer.Data
{
    public interface IPlayerReader
    {
        List<Player> GetAll();
        List<TopSpenderPlayer> GetTopSpenders(int count);
    }
    public class PlayerReader : DataAccesBase, IPlayerReader
    {
        public PlayerReader() : base()
        {
            
        }
        public List<Player> GetAll()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                List<Player> players = new();
                players = connection.Query<Player>("spGetAllPlayers", commandType: CommandType.StoredProcedure).ToList();
                return players;
            }
        }
        public List<TopSpenderPlayer> GetTopSpenders(int count)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                List<TopSpenderPlayer> players = new();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Count", count);
                players = connection.Query<TopSpenderPlayer>("spGetTopSpenders", parameters, commandType: CommandType.StoredProcedure).ToList();
                return players;
            }
        }
    }
}
