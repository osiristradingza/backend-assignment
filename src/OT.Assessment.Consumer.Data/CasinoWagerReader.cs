using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using OT.Assessment.Consumer.Api.Entities;
using System.Data;
using System.Data.SqlClient;

namespace OT.Assessment.Consumer.Data
{
    public interface ICasinoWagerReader
    {
        JObject GetCasinoWagersByAccountId(Guid aPlayerAccountId);
        List<CasinoWager> GetAll();
    }
    public class CasinoWagerReader : DataAccesBase, ICasinoWagerReader
    {
        public CasinoWagerReader() : base()
        {
            
        }
        public JObject GetCasinoWagersByAccountId(Guid aPlayerAccountId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                List<CasinoWager> wagers = new();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("AccountId", aPlayerAccountId);
                dynamic result = connection.Query<string>("spGetCasinoWagersByAccountId", parameters, commandType: CommandType.StoredProcedure);
                
                if (result != null)
                {
                    string resultString = result[0];
                    return JObject.Parse(resultString.Substring(1, resultString.LastIndexOf("]") - 1));
                }
                
                return null;
            }
        }
        public List<CasinoWager> GetAll()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection")))
            {
                List<CasinoWager> wagers = new();
                wagers = connection.Query<CasinoWager>("spGetAllCasinoWagers", commandType: CommandType.StoredProcedure).ToList();
                return wagers;
            }
        }

    }
}
