using Dapper;
using Microsoft.Extensions.Configuration;
using OT.Assessment.Consumer.Api.Entities;
using System.Data;
using System.Data.SqlClient;

namespace OT.Assessment.Consumer.Data
{
    public interface IGameReader
    {
        Game GetGameByExternaReferenceId(Guid externaReferenceId);
    }
    public class GameReader : DataAccesBase, IGameReader
    {
        public GameReader() : base()
        {
            
        }
        public Game GetGameByExternaReferenceId(Guid externaReferenceId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ExternaReferenceId", externaReferenceId);
                
                return connection.QuerySingle<Game>("spGetGameByExternaReferenceId", parameters, commandType: CommandType.StoredProcedure);
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
