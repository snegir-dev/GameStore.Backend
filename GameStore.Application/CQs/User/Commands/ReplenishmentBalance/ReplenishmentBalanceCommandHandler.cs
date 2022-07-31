using AuthorizeNet.Api.Contracts.V1;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Payments;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GameStore.Application.CQs.User.Commands.ReplenishmentBalance;

public class ReplenishmentBalanceCommandHandler
    : IRequestHandler<ReplenishmentBalanceCommand, string>
{
    private readonly IConfiguration _configuration;
    private readonly IGameStoreDbContext _context;

    public ReplenishmentBalanceCommandHandler(IConfiguration configuration,
        IGameStoreDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<string> Handle(ReplenishmentBalanceCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.UserId);

        var authenticationType = new merchantAuthenticationType()
        {
            name = _configuration["MerchantInformation:ApiLoginId"],
            Item = _configuration["MerchantInformation:TransactionKey"],
            ItemElementName = ItemChoiceType.transactionKey
        };

        var creditCard = new creditCardType
        {
            cardNumber = request.CardNumber,
            expirationDate = request.ExpirationDate,
            cardCode = request.CardCode
        };

        var billingAddress = new customerAddressType
        {
            firstName = user.UserName,
            email = user.Email
        };

        var paymentType = new paymentType { Item = creditCard };

        var listReplenishment = new lineItemType[]
        {
            new()
            {
                itemId = "1",
                name = "Balance",
                quantity = 1,
                unitPrice = request.ReplenishmentAmount
            }
        };

        var transactionRequest = new transactionRequestType
        {
            transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
            amount = 1,
            payment = paymentType,
            lineItems = listReplenishment,
            billTo = billingAddress
        };
        
        var payment = new AuthorizePayment.AuthorizePaymentBuilder()
            .AddRunEnvironment(AuthorizeNet.Environment.SANDBOX)
            .AddMerchantAuthentication(authenticationType)
            .AddTransactionController(transactionRequest)
            .Build();
        
        payment.Controller.Execute();
        var response = payment.Controller.GetApiResponse();
        
        payment.ExceptionChecking();
        
        user.Balance += request.ReplenishmentAmount;
        await _context.SaveChangesAsync(cancellationToken);
        
        var responseText = $"Success, Auth Code : {response.transactionResponse.authCode}";

        return responseText;
    }
}