using FluentValidation;

namespace GameStore.Application.CQs.Role.Queries.GetRole;

public class GetRoleQueryValidator : AbstractValidator<GetRoleQuery>
{
    public GetRoleQueryValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
    }
}