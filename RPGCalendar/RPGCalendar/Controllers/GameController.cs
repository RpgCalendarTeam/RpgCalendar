namespace RPGCalendar.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core.Dto;
    using Core.Exceptions;
    using Core.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        public GameController(IGameService service)
        {
            this._gameService = service;
        }

        [HttpPost("add/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddGame(int id)
        {
            Game? game = await _gameService.AddNew(id);
            if (game is null)
                return NotFound();
            return Ok(game);
        }

        [HttpPost("add/{id},{pClass},{pBio}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddGame(int id, string pClass, string pBio)
        {
            Game? game = await _gameService.AddNew(id, pClass, pBio);
            if (game is null)
                return NotFound();
            return Ok(game);
        }

        [HttpGet]
        public async Task<IEnumerable<Game>> Get() => await _gameService.GetForUserAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Game>> Get(int id)
        {
            try
            {
                Game? entity = await _gameService.GetByIdForUserAsync(id);
                if (entity is null)
                {
                    return NotFound();
                }

                return Ok(entity);
            }
            catch (UserPermissionException)
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Game?>> Put(int id, GameInput value)
        {
            try
            {
                var result = await _gameService.UpdateAsync(id, value);
                if (result is null)
                    return NotFound();
                return Ok(result);
            }
            catch (UserPermissionException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Game?>> Post(GameInput input)
        {
            try
            {
                var result = await _gameService.CreateAsync(input);
                return Ok(result);
            }
            catch (UserPermissionException)
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _gameService.DeleteAsync(id))
                {
                    return Ok();
                }
            }
            catch (UserPermissionException)
            {
                return Unauthorized();
            }

            return NotFound();
        }

    }
}
