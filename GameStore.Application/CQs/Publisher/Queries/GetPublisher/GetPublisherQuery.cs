using MediatR;

namespace GameStore.Application.CQs.Publisher.Queries.GetPublisher;

public class GetPublisherQuery : IRequest<PublisherVm>
{
    public long Id { get; set; }
}