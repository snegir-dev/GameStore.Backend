using MediatR;

namespace GameStore.Application.CQs.Publisher.Commands.Delete;

public class DeletePublisherCommand : IRequest
{
    public long Id { get; set; }
}