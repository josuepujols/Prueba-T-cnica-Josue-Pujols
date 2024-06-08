namespace RouletteGame.Core.Application.Dtos;

public class BetNumberAndColorRequestDto : BetRequestDto
{
    public double NumberSelected { get; set; }
    public double NumberRoulette { get; set; }
}

