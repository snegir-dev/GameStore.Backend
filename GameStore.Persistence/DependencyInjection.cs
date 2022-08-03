using GameStore.Application.Interfaces;
using GameStore.Persistence.Contexts;
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
        
        var connectionStringLog = configuration["DbLogConnection"];
        services.AddDbContext<LogDbContext>(options =>
        {
            options.UseNpgsql(connectionStringLog);
        });

        services.AddScoped<IGameStoreDbContext, GameStoreDbContext>();
        services.AddScoped<LogDbContext>();

        return services;
    }
}