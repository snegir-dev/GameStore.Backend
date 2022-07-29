using FluentValidation;

namespace GameStore.Application.CQs.User.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
    }
}