using MediatR;

namespace GameStore.Application.CQs.Publisher.Commands.Update;

public class UpdatePublisherCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateFoundation { get; set; }
}