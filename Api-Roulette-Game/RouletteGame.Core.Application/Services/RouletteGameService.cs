using RouletteGame.Core.Application.Dtos;
using RouletteGame.Core.Application.Interfaces;
using RouletteGame.Core.Application.Shared.ApiResponses;
using System.Net;

namespace RouletteGame.Core.Application.Services;

public class RouletteGameService : IRouletteGameService
{
    private static readonly string[] Colors = ["Red", "Black"];

    public ResultT<RouletteDto> GetRoulette()
    {
        //Generate a number between 0 and 36
        Random rnd = new();
        int number = rnd.Next(0, 36);

        //Return a color between Red and Black
        int indexColor = rnd.Next(0, 2);
        string color = Colors[indexColor];

        RouletteDto rouletteDto = new(number, color);
        return new ResultT<RouletteDto>(string.Empty, true, HttpStatusCode.OK, rouletteDto);
    }

    public ResultT<BetResultDto> BetColor(BetRequestDto betRequestDto)
    {
        if(string.Equals(betRequestDto.ColorSelected, betRequestDto.ColorRoulette, StringComparison.CurrentCultureIgnoreCase))
        {
            var halfBet = betRequestDto.Bet / 2;
            var sumAmount = betRequestDto.Amount + halfBet;

            BetResultDto resultDto = new(sumAmount, true);
            return new ResultT<BetResultDto>("You won the bet", true, HttpStatusCode.OK, resultDto);
        }
        else
        {
           return MissedBet(betRequestDto.Amount, betRequestDto.Bet);
        }
        
    }

    public ResultT<BetResultDto> BetNumberAndColor(BetNumberAndColorRequestDto betNumerAndColorDto)
    {
        if ((betNumerAndColorDto.NumberSelected == betNumerAndColorDto.NumberRoulette) 
            && betNumerAndColorDto.ColorSelected.Equals(betNumerAndColorDto.ColorRoulette, StringComparison.CurrentCultureIgnoreCase))
        {
            var amountWon = betNumerAndColorDto.Bet * 3;
            var sumAmount = betNumerAndColorDto.Amount + amountWon;

            BetResultDto resultDto = new(sumAmount, true);
            return new ResultT<BetResultDto>("You won the bet", true, HttpStatusCode.OK, resultDto);
        }
        else
        {
           return MissedBet(betNumerAndColorDto.Amount, betNumerAndColorDto.Bet);
        }
    }

    public ResultT<BetResultDto> BetOddOrEven(BetOddOrEvenRequestDto requestDto)
    {
        var modulo = requestDto.NumberRoulette % 2;
        if(((!requestDto.IsOdd && modulo == 0) || (requestDto.IsOdd && modulo != 0))
            && requestDto.ColorSelected.Equals(requestDto.ColorRoulette, StringComparison.CurrentCultureIgnoreCase))
        {
            requestDto.Amount += requestDto.Bet;

            BetResultDto resultDto = new(requestDto.Amount, true);
            return new ResultT<BetResultDto>("You won the bet", true, HttpStatusCode.OK, resultDto);
        }

        return MissedBet(requestDto.Amount, requestDto.Bet);
    }

    private ResultT<BetResultDto> MissedBet(double amount, double bet)
    {
        var restAmount = amount - bet;
        BetResultDto resultDto = new(restAmount, false);
        return new ResultT<BetResultDto>("You missed the bet", true, HttpStatusCode.OK, resultDto);
    }


}
