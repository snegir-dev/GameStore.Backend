using FluentValidation;

namespace GameStore.Application.CQs.Genre.Queries.GetGenre;

public class GetGenreQueryValidator : AbstractValidator<GetGenreQuery>
{
    public GetGenreQueryValidator()
    {
        RuleFor(g => g.Id).NotEmpty();
    }
}