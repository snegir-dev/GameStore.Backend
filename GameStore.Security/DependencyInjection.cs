using GameStore.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Security;

public static class DependencyInjection
{
    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        return services.AddScoped<IJwtGenerator, JwtGenerator>();
    }
}