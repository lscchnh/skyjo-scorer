using Microsoft.AspNetCore.Mvc;
using Models.Dto;
using Services;

namespace Controllers;

[ApiController]
[Route("api/players")]
public class PlayerController(IPlayerService playerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PlayerDto>>> GetPlayerStats([FromQuery] int? year)
    {
        return Ok(await playerService.GetAllWithStats(year));
    }
}

