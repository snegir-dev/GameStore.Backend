using System.Security.Claims;
using GameStore.Domain;

namespace GameStore.Application.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(User user);
    string CreateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}