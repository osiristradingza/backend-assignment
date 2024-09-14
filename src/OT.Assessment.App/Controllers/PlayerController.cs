using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Database.Abstract;
using OT.Assessment.Manager.UseCases.Wagers.Interface;
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

        [HttpGet]
        [Route("GetWagers")]
        public async Task<IActionResult> GetWagers() => Ok(await _wagerManager.GetPlayerWagersAsync());
        
    }
}
