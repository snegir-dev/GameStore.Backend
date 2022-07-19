using GameStore.Domain;

namespace GameStore.Application.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(User user);
}