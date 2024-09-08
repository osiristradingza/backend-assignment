using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Consumer.Api;
using OT.Assessment.Consumer.Api.Entities;
namespace OT.Assessment.App.Controllers
{
  
    [ApiController]
    public class CasinoWagerController : ControllerBase
    {
        private readonly ILogger<CasinoWagerController> _logger; 
        private readonly ICasinoWagerService _casinoWagerService;
        private readonly IGameService _gameService;

        public CasinoWagerController(ICasinoWagerService casinoWagerService, ILogger<CasinoWagerController> logger, IGameService gameService)
        {
            _logger = logger;
            _casinoWagerService = casinoWagerService;
            _gameService = gameService;
        }
        
        [HttpGet("~/wagers")]
        public string Get(Guid accountId)
        {
            return _casinoWagerService.GetCasinoWagersByPlayerAccountId(accountId).ToString();
        }
        
        [HttpGet("~/casinowagers")]
        public List<CasinoWager> Get()
        {
            return _casinoWagerService.GetAll();
        }
        
        [HttpPost("~/casinowager")]
        public void SaveWager([FromBody] CasinoWager wager)
        {
            try
            {
                var game = _gameService.GetGameByExternaReferenceId(wager.ExternalReferenceId);
                var gameStartTime = game?.StartDateTime;
                var gameEndTime = game?.EndDateTime;
                if (wager.Amount <= 0)
                    throw new Exception("Wager amount cannot be less than 1");
                if(wager.CreatedDateTime > DateTime.Now)
                    throw new Exception("Wager created date cannot be future dated");
                if (gameStartTime.HasValue && wager.CreatedDateTime >= gameStartTime)
                    throw new Exception("Wager created date cannot be after game start time");
                if (gameEndTime.HasValue && wager.CreatedDateTime >= gameEndTime)
                    throw new Exception("Wager created date cannot be after game has ended");
                _casinoWagerService.PublishMessage(wager);
                _logger.LogInformation("Wager published to queue {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                _casinoWagerService.SaveWager(wager);
                _logger.LogInformation("Wager Created {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
