namespace RouletteGame.Core.Application.Dtos;

public class BetOddOrEvenRequestDto : BetRequestDto
{
    public int NumberRoulette { get; set; }
    public bool IsOdd { get; set; }

}