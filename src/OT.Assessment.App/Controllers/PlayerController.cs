using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Database.Abstract;
using OT.Assessment.Manager.UseCases.Wagers.Interface;
using OT.Assessment.Model;
namespace OT.Assessment.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IWagerManager _wagerManager;
        public PlayerController(IWagerManager wagerManager) 
        {
            _wagerManager = wagerManager;
        }
        //POST api/player/casinowager

        //GET api/player/{playerId}/wagers

        //GET api/player/topSpenders?count=10
        //
        [HttpPost]
        [Route("casinowager")]
        public async Task<IActionResult> AddWager(AddCasinoWagerRequest addCasinoWager) 
        {
            return Ok();

        }

        [HttpGet]
        [Route("GetWagers")]
        public async Task<IActionResult> GetWagers() => Ok(await _wagerManager.GetPlayerWagersAsync());

        [HttpGet]
        [HttpGet("{playerId}/casino")]
        public async Task<IActionResult> Wagers(Guid playerId, int page = 1, int pageSize = 10) 
        {
            return Ok();

        }

        [HttpGet("topSpenders")]
        public async Task<IActionResult> GetTopSpenders(int count = 10) 
        {
            return Ok();

        }

    }
}
