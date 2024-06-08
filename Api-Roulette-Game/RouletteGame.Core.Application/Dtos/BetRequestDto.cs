namespace RouletteGame.Core.Application.Dtos;

public class BetRequestDto
{
    public double Amount { get; set; }
    public double Bet { get; set; }
    public string ColorSelected { get; set; } = string.Empty;
    public string ColorRoulette { get; set; } = string.Empty;
}
