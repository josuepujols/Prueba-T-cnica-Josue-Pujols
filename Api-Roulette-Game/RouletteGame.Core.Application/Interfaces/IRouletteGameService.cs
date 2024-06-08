using RouletteGame.Core.Application.Dtos;
using RouletteGame.Core.Application.Shared.ApiResponses;

namespace RouletteGame.Core.Application.Interfaces;

public interface IRouletteGameService
{
    ResultT<RouletteDto> GetRoulette();
    ResultT<BetResultDto> BetColor(BetRequestDto betRequestDto);
    ResultT<BetResultDto> BetNumberAndColor(BetNumberAndColorRequestDto betNumerAndColorDto);
    ResultT<BetResultDto> BetOddOrEven(BetOddOrEvenRequestDto requestDto);
}
