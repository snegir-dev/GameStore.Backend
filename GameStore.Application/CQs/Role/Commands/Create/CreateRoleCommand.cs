using MediatR;

namespace GameStore.Application.CQs.Role.Commands.Create;

public class CreateRoleCommand : IRequest<long>
{
    public string Name { get; set; }
}