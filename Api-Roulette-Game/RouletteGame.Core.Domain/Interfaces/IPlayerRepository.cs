using RouletteGame.Core.Domain.Entities;

namespace RouletteGame.Core.Domain.Interfaces;

public interface IPlayerRepository
{

    Task Add(Player newPlayer);
    Task<Player?> GetByName(string name);
    Task Update(Player updatePlayer);
}
