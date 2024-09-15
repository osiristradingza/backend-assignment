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
using OT.Assessment.Model;
using OT.Assessment.Database.DTO;

namespace OT.Assessment.Database.Abstract
{
    public class WagersRepository: IWagers
    {
        private readonly ILogger<WagersRepository> _logger;
        private readonly IDatabaseConnection _databaseConnection;
        public WagersRepository(ILogger<WagersRepository> logger, IDatabaseConnection databaseConnection)
        {
            _logger = logger;
            _databaseConnection = databaseConnection;
        }

        // Get all wagers using stored procedure
        public async Task<IEnumerable<PlayerWagers>> GetAllWagersAsync()
        {
            try
            {
                using (var connection = _databaseConnection.CreateConnection())
                {
                    var wagers = await connection.QueryAsync<Wagers>("sp_GetAllWages", commandType: CommandType.StoredProcedure);
                    _logger.LogInformation($"{DateTime.Now }- {nameof(WagersRepository)} - {nameof(GetAllWagersAsync)} - {wagers.Count()} retrieved.");
                    return PlayerWagerMapper.ListMapTo(wagers);
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError($"{DateTime.Now} - SQL Exception: {nameof(WagersRepository)} - {nameof(GetAllWagersAsync)} - {ex.Message}");
                throw new Exception("An error occurred while retrieving the wagers. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(WagersRepository)} - {nameof(GetAllWagersAsync)} - {ex.Message}");
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
