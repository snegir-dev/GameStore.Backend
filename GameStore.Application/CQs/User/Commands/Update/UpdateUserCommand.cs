using MediatR;

namespace GameStore.Application.CQs.User.Commands.Update;

public class UpdateUserCommand : IRequest
{
    public long? Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}