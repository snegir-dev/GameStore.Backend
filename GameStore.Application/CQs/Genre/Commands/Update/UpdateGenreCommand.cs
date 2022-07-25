using MediatR;

namespace GameStore.Application.CQs.Genre.Commands.Update;

public class UpdateGenreCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
}