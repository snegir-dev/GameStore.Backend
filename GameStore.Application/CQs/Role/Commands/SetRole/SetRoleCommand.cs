using MediatR;

namespace GameStore.Application.CQs.Role.Commands.SetRole;

public class SetRoleCommand : IRequest
{
    public long? CurrentUserId { get; set; }
    public long? UserId { get; set; }
    public long RoleId { get; set; }
}