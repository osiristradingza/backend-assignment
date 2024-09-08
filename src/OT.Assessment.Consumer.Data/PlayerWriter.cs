using Dapper;
using Microsoft.Extensions.Configuration;
using OT.Assessment.Consumer.Api.Entities;
using System.Data;
using System.Data.SqlClient;

namespace OT.Assessment.Consumer.Data
{
    public interface IPlayerWriter
    {
        void Save(Player aPlayer);
    }
    public class PlayerWriter : DataAccesBase, IPlayerWriter
    {
        public PlayerWriter() : base() 
        {
            
        }
        public void Save(Player aPlayer)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                var rowsAffected = connection.Execute("spInsertPlayer", aPlayer, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
