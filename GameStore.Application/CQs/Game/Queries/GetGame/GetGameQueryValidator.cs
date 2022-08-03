using FluentValidation;

namespace GameStore.Application.CQs.Game.Queries.GetGame;

public class GetGameQueryValidator : AbstractValidator<GetGameQuery>
{
    public GetGameQueryValidator()
    {
        RuleFor(g => g.Id).NotEmpty();
    }
}