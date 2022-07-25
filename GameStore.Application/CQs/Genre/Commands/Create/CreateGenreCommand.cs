using MediatR;

namespace GameStore.Application.CQs.Genre.Commands.Create;

public class CreateGenreCommand : IRequest<long>
{
    public string Name { get; set; }
}