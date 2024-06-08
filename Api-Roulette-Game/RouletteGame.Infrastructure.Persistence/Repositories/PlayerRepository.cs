using Microsoft.EntityFrameworkCore;
using RouletteGame.Core.Domain.Entities;
using RouletteGame.Core.Domain.Interfaces;

namespace RouletteGame.Infrastructure.Persistence.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly MainDbContext _db;
    public PlayerRepository(MainDbContext db)
    {
        _db = db;
    }

    public async Task Add(Player newPlayer)
    {
        _ = _db.Players.Add(newPlayer);
        await _db.SaveChangesAsync();
    }

    public async Task<Player?> GetByName(string name) => await _db.Players
        .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

    public async Task Update(Player updatePlayer)
    {
        _ = _db.Players.Update(updatePlayer);
        await _db.SaveChangesAsync();
    }
}
