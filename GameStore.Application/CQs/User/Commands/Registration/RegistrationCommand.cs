using MediatR;

namespace GameStore.Application.CQs.User.Commands.Registration;

public class RegistrationCommand : IRequest<AuthenticatedResponse>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}