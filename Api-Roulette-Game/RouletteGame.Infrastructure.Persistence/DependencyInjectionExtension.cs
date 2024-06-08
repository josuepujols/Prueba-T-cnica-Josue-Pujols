using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RouletteGame.Core.Domain.Interfaces;
using RouletteGame.Infrastructure.Persistence.Repositories;

namespace RouletteGame.Infrastructure.Persistence;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructurePersistenceServices(this IServiceCollection services)
    {
        // Configures Entity Framework Core to use a SQLite database located in the local application data folder.
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        services.AddDbContext<MainDbContext>(opt =>
        {
            opt.UseSqlite($"Data Source={Path.Join(path, "housesCourse.db")}", m => 
                m.MigrationsAssembly(typeof(MainDbContext).Assembly.FullName));
        });

        //Add injection for the repository
        services.AddScoped<IPlayerRepository, PlayerRepository>();

        return services;
    }
}
