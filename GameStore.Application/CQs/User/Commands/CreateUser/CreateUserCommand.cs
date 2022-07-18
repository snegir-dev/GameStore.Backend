using MediatR;

namespace GameStore.Application.CQs.User.Commands.CreateUser;

public class CreateUserCommand : IRequest<Unit>
{
    public string Login { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}