using Microsoft.AspNetCore.Mvc;
using RouletteGame.Core.Application.Dtos;
using RouletteGame.Core.Application.Interfaces;
using RouletteGame.Core.Application.Shared.ApiResponses;

namespace RouletteGame.Controllers;

[ApiController]
[Route("api/roulette")]
public class RouletteGameController : ControllerBase
{
    private readonly IRouletteGameService _gameService;
    public RouletteGameController(IRouletteGameService gameService)
    {
        _gameService = gameService;
    }

    /// <summary>
    /// Endpoint that returns a random number between 6 and 36, and a color between red and black.
    /// </summary>
    /// <returns></returns>
    [HttpGet("openGame")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ResultT<RouletteDto>> GetRoulette() => Ok(_gameService.GetRoulette());

    /// <summary>
    /// Endpoint to make a bet on the color and returns if you won or lost and the amount of the bet 
    /// </summary>
    /// <param name="betRequestDto"></param>
    /// <returns></returns>
    [HttpPost("betColor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ResultT<BetResultDto>> BetColor([FromBody] BetRequestDto betRequestDto) => Ok(_gameService.BetColor(betRequestDto));

    /// <summary>
    /// Endpoint to make a bet by roulette number, roulette color, player number and player color, 
    /// and indicates whether or not the player won the bet, and indicating the amount won or lost if so.
    /// </summary>
    /// <param name="betDto"></param>
    /// <returns></returns>
    [HttpPost("betNumberAndColor")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ResultT<BetResultDto>> BetNumberAndColor([FromBody] BetNumberAndColorRequestDto betDto) => 
        Ok(_gameService.BetNumberAndColor(betDto));

    /// <summary>
    /// Endpoint that receives the roulette number, roulette color, and whether the user bet on even or odd, and also receives the color. 
    /// If the user correctly guesses that the number is even or odd and also the color, the amount won is returned.
    /// </summary>
    /// <param name="betDto"></param>
    /// <returns></returns>
    [HttpPost("betOddOrEven")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ResultT<BetResultDto>> BetOddOrEven([FromBody] BetOddOrEvenRequestDto betDto) =>
        Ok(_gameService.BetOddOrEven(betDto));
}
