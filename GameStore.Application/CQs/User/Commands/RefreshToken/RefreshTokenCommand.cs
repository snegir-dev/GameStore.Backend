using MediatR;

namespace GameStore.Application.CQs.User.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthenticatedResponse>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}