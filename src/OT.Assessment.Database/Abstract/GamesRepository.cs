using Dapper;
using Microsoft.Extensions.Logging;
using OT.Assessment.Database.Helper;
using OT.Assessment.Database.Interface;
using OT.Assessment.Database.Tables;
using OT.Assessment.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Database.Abstract
{
    public class GamesRepository : IGames
    {
        private readonly ILogger<GamesRepository> _logger;
        private readonly IDatabaseConnection _databaseConnection;
        public GamesRepository(ILogger<GamesRepository> logger, IDatabaseConnection databaseConnection)
        {
            _logger = logger;
            _databaseConnection = databaseConnection;
        }
        public async Task<Guid?> AddProviderAsync(AddProviderRequest addProviderRequest)
        {
            try
            {
                using (var connection = _databaseConnection.CreateConnection())
                {
                    _logger.LogInformation($"{DateTime.Now} - {nameof(GamesRepository)} - {nameof(AddProviderAsync)} - attempting to add new provider {addProviderRequest.ProviderName}.");
                    
                    // Generate new ProviderID (GUID)
                    var newProviderID = Guid.NewGuid();

                    var parameters = new
                    {
                        ProviderID = newProviderID,  // Pass the new GUID
                        Name = addProviderRequest.ProviderName
                    };

                    // Execute the stored procedure and retrieve the same ProviderID back
                    var providerID = await connection.ExecuteScalarAsync<Guid?>(
                        "sp_AddProvider",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    _logger.LogInformation($"{DateTime.Now} - {nameof(GamesRepository)} - {nameof(AddProviderAsync)} - provider added with ID {providerID}.");

                    // Return the new ProviderID
                    return providerID;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError($"{DateTime.Now} - SQL Exception: {nameof(GamesRepository)} - {nameof(AddProviderAsync)} - {ex.Message}");
                throw new Exception("An error occurred while adding the provider. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(GamesRepository)} - {nameof(AddProviderAsync)} - {ex.Message}");
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
        }
        public async Task<Guid?> AddGameAsync(AddGameRequest addGameRequest) 
        {
            try
            {
                using (var connection = _databaseConnection.CreateConnection())
                {
                    _logger.LogInformation($"{DateTime.Now} - {nameof(GamesRepository)} - {nameof(AddGameAsync)} - attempting to add new game {addGameRequest.GameName}.");

                    var newGameID = Guid.NewGuid();
                    var parameters = new
                    {
                        GameID = newGameID,
                        Name = addGameRequest.GameName,
                        GameDescription = addGameRequest.GameDescription,
                        Theme = addGameRequest.Theme,
                        ProviderName = addGameRequest.ProviderName
                    };

                    var gameID = await connection.ExecuteScalarAsync<Guid?>(
                            "sp_AddGame",
                            parameters,
                            commandType: CommandType.StoredProcedure
                        );

                    _logger.LogInformation($"{DateTime.Now} - {nameof(GamesRepository)} - {nameof(AddGameAsync)} - game added with ID {gameID}.");

                    return gameID;
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError($"{DateTime.Now} - SQL Exception: {nameof(GamesRepository)} - {nameof(AddGameAsync)} - {ex.Message}");
                throw new Exception("An error occurred while adding the game. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(GamesRepository)} - {nameof(AddGameAsync)} - {ex.Message}");
                throw new Exception("An unexpected error occurred. Please try again later.");
            }

        }
    }
}
