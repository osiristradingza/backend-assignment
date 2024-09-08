using Microsoft.AspNetCore.Mvc;
using OT.Assessment.Consumer.Api;
using OT.Assessment.Consumer.Api.Entities;
namespace OT.Assessment.App.Controllers
{
  
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        
        [HttpGet("~/topSpenders")]
        public List<TopSpenderPlayer> Get(int count)
        {
            return _playerService.GetTopSpenders(count);
        }
        [HttpGet("~/players")]
        public List<Player> Get()
        {
            return _playerService.GetAll();
        }

        [HttpPost("~/player")]
        public void SavePlayer([FromBody] Player player)
        {
            _playerService.SavePlayer(player);
        }
    }
}
