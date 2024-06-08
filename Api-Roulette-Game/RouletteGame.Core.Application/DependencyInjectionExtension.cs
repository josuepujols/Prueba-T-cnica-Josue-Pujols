using Microsoft.Extensions.DependencyInjection;
using RouletteGame.Core.Application.Interfaces;
using RouletteGame.Core.Application.Services;

namespace RouletteGame.Core.Application;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        //Injection del servicio
        services.AddTransient<IPlayerService, PlayerService>();
        services.AddTransient<IRouletteGameService, RouletteGameService>();

        return services;
    }
}
