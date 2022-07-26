using MediatR;

namespace GameStore.Application.CQs.Role.Queries.GetRole;

public class GetRoleQuery : IRequest<RoleVm>
{
    public long Id { get; set; }
}