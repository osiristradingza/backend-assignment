using Microsoft.AspNetCore.Mvc;
using OT.Assessment.App.Models.DTOs;
using OT.Assessment.App.Services;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;

namespace OT.Assessment.App.Controllers
{
    [ApiController]
    [Route("api/player/")]
    public class PlayerController : ControllerBase
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<PlayerController> _logger;
        private readonly CasinoWagerService _casinoWagerService;

        public PlayerController(IConnectionFactory connectionFactory, ILogger<PlayerController> logger, CasinoWagerService casinoWagerService)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            _casinoWagerService = casinoWagerService;
        }

        [HttpPost]
        [Route("casinowager")]
        public async Task<IActionResult> AddCasinoWager([FromBody] CasinoWagerPackage package)
        {
            try
            {
                await _casinoWagerService.AddCasinoWagerAsync(package);
                return Ok(new { Status = "Wager added successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET api/player/{playerId}/wagers
        [HttpGet]
        [Route("{playerId}/wagers")]
        public async Task<IActionResult> GetCasinoWagersAsync([FromRoute] Guid playerId, int pageSize, int page = 1)
        {
            try
            {
                var (wagers, total, totalPages) = await _casinoWagerService.GetCasinoWagersAsync(playerId, pageSize, page);

                if (wagers == null)
                {
                    return NotFound($"No data was found for playerId {playerId}");
                }

                return Ok(new { data = wagers, page = page, pageSize = pageSize, total = total, totalPages = totalPages });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the data for playerId: {PlayerId}", playerId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET api/player/topSpenders?count=10
        [HttpGet]
        [Route("topSpenders")]
        public async Task<IActionResult> GetTopSpenders([FromQuery] int count)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("Count must be greater than 0");
                }

                var topSpenders = await _casinoWagerService.GetTopSpendersAsync(count);
                return Ok(new { topSpenders });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching top spenders.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
