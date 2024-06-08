
using System.ComponentModel.DataAnnotations;

namespace RouletteGame.Core.Application.Dtos;

public record PlayerDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public double Amount { get; set; }

    public PlayerDto(string name, double amount)
    {
        Name = name;
        Amount = amount;
    }
};
