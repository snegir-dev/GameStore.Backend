using MediatR;

namespace GameStore.Application.CQs.User.Commands.ReplenishmentBalance;

public class ReplenishmentBalanceCommand : IRequest<string>
{
    public long? UserId { get; set; }
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string CardCode { get; set; }
    public long ReplenishmentAmount { get; set; }
}