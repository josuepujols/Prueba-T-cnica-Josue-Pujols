using Microsoft.AspNetCore.Mvc;
using RouletteGame.Core.Application.Dtos;
using RouletteGame.Core.Application.Interfaces;
using RouletteGame.Core.Application.Shared.ApiResponses;
using System.Net;

namespace RouletteGame.Controllers;

[ApiController]
[Route("api/roulette/player")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    /// <summary>
    /// Endpoint that returns a player searched by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [HttpGet("{name}", Name = "GetPlayerByName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResultT<PlayerDto>>> GetPlayerByName(string name)
    {
        var player = await _playerService.GetByName(name);

        if (!player.Success)
            return NotFound(player);

        return Ok(player);
    }

    /// <summary>
    /// Endpoint to add a new player or edit it if it already exists.
    /// </summary>
    /// <param name="playerDto"></param>
    /// <returns></returns>
    [HttpPost("addOrUpdate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Result>> AddOrUpdate([FromBody] PlayerDto playerDto)
    {
        var playerExisted = await _playerService.GetByName(playerDto.Name);

        if (!playerExisted.Success)
        {
            var playerAdded = await _playerService.Add(playerDto);

            if (!playerAdded.Success && playerAdded.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(playerAdded);
            }
            else if (playerAdded.StatusCode == HttpStatusCode.InternalServerError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, playerAdded);
            }

            return CreatedAtRoute("GetPlayerByName", new { name = playerDto.Name }, playerAdded);
        }

        var playerUpdated = await _playerService.Update(playerDto);

        if (!playerUpdated.Success && playerUpdated.StatusCode == HttpStatusCode.BadRequest)
        {
            return BadRequest(playerUpdated);
        }
        else if (playerUpdated.StatusCode == HttpStatusCode.InternalServerError)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, playerUpdated);
        }

        return Ok(playerUpdated);
    }
}
