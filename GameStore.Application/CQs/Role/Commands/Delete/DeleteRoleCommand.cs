using MediatR;

namespace GameStore.Application.CQs.Role.Commands.Delete;

public class DeleteRoleCommand : IRequest
{
    public long Id { get; set; }
}