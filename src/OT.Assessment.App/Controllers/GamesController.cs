using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Database.Interface;
using OT.Assessment.Model;

namespace OT.Assessment.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGames _games;
        public GamesController(IGames games) 
        {
            _games = games;
        }

        [HttpPost]
        [Route("AddProvider")]
        public async Task<IActionResult> AddProvider(AddProviderRequest addProviderRequest) => Ok(await _games.AddProviderAsync(addProviderRequest));

        [HttpPost]
        [Route("AddGame")]
        public async Task<IActionResult> AddGame(AddGameRequest addGameRequest) => Ok(await _games.AddGameAsync(addGameRequest));
    }
}
