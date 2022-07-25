using MediatR;

namespace GameStore.Application.CQs.Genre.Queries.GetGenre;

public class GetGenreQuery : IRequest<GenreVm>
{
    public long Id { get; set; }
}