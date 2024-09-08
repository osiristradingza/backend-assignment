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
        private readonly Guid DEFAULT_GAME_EXTERNAL_REF;

        public CasinoWagerController(ICasinoWagerService casinoWagerService, ILogger<CasinoWagerController> logger, IGameService gameService)
        {
            _logger = logger;
            _casinoWagerService = casinoWagerService;
            _gameService = gameService;
            DEFAULT_GAME_EXTERNAL_REF = Guid.Parse("49D7FD17-EEEC-4836-A967-4E09DA37E2B9");
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
                //GetGameByExternaReferenceId to use wager.ExternalReferenceId for parameter below
                var game = _gameService.GetGameByExternaReferenceId(DEFAULT_GAME_EXTERNAL_REF);

                DateTime? gameStartTime = game != null ? game.StartDateTime : null;
                DateTime? gameEndTime = game != null ? game.EndDateTime : null;

                if (wager.Amount <= 0)
                    throw new Exception("Wager amount cannot be less than 1");
                if (wager.NumberOfBets <= 0)
                    throw new Exception("Number of bets cannot be less than 1");
                if (wager.CreatedDateTime > DateTime.Now)
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
