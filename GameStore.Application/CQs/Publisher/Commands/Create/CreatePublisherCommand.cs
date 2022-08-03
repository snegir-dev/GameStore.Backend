using MediatR;

namespace GameStore.Application.CQs.Publisher.Commands.Create;

public class CreatePublisherCommand : IRequest<long>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateFoundation { get; set; }
}