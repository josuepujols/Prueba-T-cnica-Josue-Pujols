using RouletteGame.Core.Application.Dtos;
using RouletteGame.Core.Application.Interfaces;
using RouletteGame.Core.Application.Shared.ApiResponses;
using RouletteGame.Core.Domain.Entities;
using RouletteGame.Core.Domain.Interfaces;
using System.Net;

namespace RouletteGame.Core.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _repo;
    public PlayerService(IPlayerRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result> Add(PlayerDto playerDto)
    {                                                                                                                              
        try
        {
            if(playerDto != null)
            {
                Player newPlayer = new()
                {
                    Name = playerDto.Name,
                    Amount = playerDto.Amount
                };

                await _repo.Add(newPlayer);

                return new Result("Player Added", true, HttpStatusCode.Created);
            }

            return new Result("Player dto must not be null", false, HttpStatusCode.BadRequest);
        }
        catch (Exception)
        {
            return new Result("Ha ocurrido un error inesperado", false, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultT<PlayerDto>> GetByName(string name)
    {
        Player? player = await _repo.GetByName(name);

        if(player != null)
        {
            PlayerDto playerDto = new(player.Name, player.Amount);

            return new ResultT<PlayerDto>("Player found", true, HttpStatusCode.OK, playerDto);
        }

        return new ResultT<PlayerDto>($"Player with name: {name} not found", false, HttpStatusCode.NotFound, null!);
    }

    public async Task<Result> Update(PlayerDto playerDto)
    {
        try
        {
            if (playerDto != null)
            {
                Player? player = await _repo.GetByName(playerDto.Name);

                if (player != null)
                {
                    player.Amount = playerDto.Amount;

                    await _repo.Update(player);

                    return new Result("Player Updated", true, HttpStatusCode.OK);
                }
                return new Result($"Player with name: {playerDto.Name} doesn't exist", false, HttpStatusCode.NotFound);
            }

            return new Result("Player dto must not be null", false, HttpStatusCode.BadRequest);
        }
        catch (Exception)
        {
            return new Result("Ha ocurrido un error inesperado", false, HttpStatusCode.InternalServerError);
        }
    }
}
