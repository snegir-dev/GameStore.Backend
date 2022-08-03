using FluentValidation;

namespace GameStore.Application.CQs.Publisher.Queries.GetPublisher;

public class GetPublisherQueryValidator : AbstractValidator<GetPublisherQuery>
{
    public GetPublisherQueryValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}