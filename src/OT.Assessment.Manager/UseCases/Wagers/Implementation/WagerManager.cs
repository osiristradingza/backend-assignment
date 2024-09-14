using Microsoft.Extensions.Logging;
using OT.Assessment.Database.Abstract;
using OT.Assessment.Database.Interface;
using OT.Assessment.Manager.UseCases.Wagers.Interface;
using OT.Assessment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Manager.UseCases.Wagers.Implementation
{
    public class WagerManager : IWagerManager
    {
        private readonly ILogger<WagerManager> _logger;
        private readonly IWages _wages;
        public WagerManager(ILogger<WagerManager> logger, IWages wages) 
        {
            _logger = logger;
            _wages = wages;
        }
        public async Task<IEnumerable<PlayerWagers>> GetPlayerWagersAsync()
        {
            try 
            {
                _logger.LogInformation($"{DateTime.Now} - {nameof(WagerManager)} - {nameof(GetPlayerWagersAsync)} - attempting to retrieve player wagers.");
                return await _wages.GetAllWagersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - General Exception: {nameof(WagerManager)} - {nameof(GetPlayerWagersAsync)} - {ex.Message}");
                throw new Exception("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
