using MediatR;

namespace GameStore.Application.CQs.Genre.Commands.Delete;

public class DeleteGenreCommand : IRequest
{
    public long Id { get; set; }
}