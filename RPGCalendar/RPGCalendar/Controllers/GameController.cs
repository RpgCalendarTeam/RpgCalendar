namespace RPGCalendar.Controllers
{
    using System.Threading.Tasks;
    using Core.Dto;
    using Core.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class GameController : BaseApiController<Game, GameInput>
    {
        private readonly IGameService gameService;
        public GameController(IGameService service) :
            base(service)
        {
            this.gameService = service;
        }

        [HttpPost("add/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddGame(int id)
        {
            Game? game = await gameService.AddNewGame(id);
            if (game is null)
                return NotFound();
            return Ok(game);
        }


    }
}
