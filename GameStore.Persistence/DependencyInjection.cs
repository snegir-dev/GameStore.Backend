using GameStore.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<GameStoreDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IGameStoreDbContext, GameStoreDbContext>();

        return services;
    }
}