using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Database.Interface;
using OT.Assessment.Manager.UseCases.Games.Interface;
using OT.Assessment.Model;

namespace OT.Assessment.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameManager _gameManager;
        public GamesController(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        [HttpPost]
        [Route("AddProvider")]
        public async Task<IActionResult> AddProvider(AddProviderRequest addProviderRequest) => Ok(await _gameManager.AddProviderAsync(addProviderRequest));

        [HttpPost]
        [Route("AddGame")]
        public async Task<IActionResult> AddGame(AddGameRequest addGameRequest) => Ok(await _gameManager.AddGameAsync(addGameRequest));

        [HttpGet]
        [Route("GetAllGames")]
        public async Task<IActionResult> GetAllGames()=>Ok(await _gameManager.GetAllGamesAsync());


        [HttpGet]
        [Route("GetAllProviders")]
        public async Task<IActionResult> GetAllProviders() => Ok(await _gameManager.GetAllProvidersAsync());

    }
}
