using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using OT.Assessment.Database.Tables;
using OT.Assessment.Database.Helper;
using OT.Assessment.Database.Interface;

namespace OT.Assessment.Database.Abstract
{
    public class WagesRepository: IWages
    {
        private readonly ILogger<WagesRepository> _logger;
        private readonly IDatabaseConnection _databaseConnection;
        public WagesRepository(ILogger<WagesRepository> logger, IDatabaseConnection databaseConnection)
        {
            _logger = logger;
            _databaseConnection = databaseConnection;
        }

        // Get all wagers using stored procedure
        public async Task<IEnumerable<Wager>> GetAllWagersAsync()
        {
            try
            {
                using (var connection = _databaseConnection.CreateConnection())
                {
                    return await connection.QueryAsync<Wager>("GetAllWagers", commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw new Exception("An error occurred while retrieving the wagers. Please try again later.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
