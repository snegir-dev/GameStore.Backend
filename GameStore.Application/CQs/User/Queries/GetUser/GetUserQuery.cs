using MediatR;

namespace GameStore.Application.CQs.User.Queries.GetUser;

public class GetUserQuery : IRequest<UserVm>
{
    public long? Id { get; set; }
}