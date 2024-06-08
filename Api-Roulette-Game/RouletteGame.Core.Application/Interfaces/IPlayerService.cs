using RouletteGame.Core.Application.Dtos;
using RouletteGame.Core.Application.Shared.ApiResponses;

namespace RouletteGame.Core.Application.Interfaces;

public interface IPlayerService
{
    Task<Result> Add(PlayerDto playerDto);
    Task<ResultT<PlayerDto>> GetByName(string name);
    Task<Result> Update(PlayerDto playerDto);
}
