using FluentValidation;

namespace GameStore.Application.CQs.Company.Queries.GetCompany;

public class GetCompanyQueryValidator : AbstractValidator<GetCompanyQuery>
{
    public GetCompanyQueryValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}