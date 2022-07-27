using MediatR;

namespace GameStore.Application.CQs.User.Queries.Login;

public class LoginQuery : IRequest<UserToken>
{
    public string Email { get; set; }
    public string Password { get; set; }
}