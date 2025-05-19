using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Services;

namespace Controllers;

[ApiController]
[Route("api/games")]
public class GamesController(IGameService gameService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddGame([FromBody] AddGameRequest addGameRequest)
    {
        await gameService.Add(addGameRequest);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<GameDto>>> GetGames([FromQuery] int? year)
    {
        return Ok(await gameService.GetAll(year));
    }

    [HttpDelete("{number}")]
    public async Task<IActionResult> DeleteGame(int number)
    {
        await gameService.Delete(number);
        return NoContent();
    }
}