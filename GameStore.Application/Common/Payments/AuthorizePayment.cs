using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using GameStore.Application.Interfaces;

namespace GameStore.Application.Common.Payments;

public class AuthorizePayment
{
    public createTransactionController Controller { get; set; }

    public void ExceptionChecking()
    {
        var errorResponse = Controller.GetErrorResponse();
        
        if (errorResponse != null && errorResponse.messages.message.Length > 0)
        {
            throw new Exception($"Error code: {errorResponse.messages.message[0].code} " +
                                $"Error message: {errorResponse.messages.message[0].text}");
        }
    }

    public class AuthorizePaymentBuilder
    {
        private readonly AuthorizePayment _authorizePayment;

        public AuthorizePaymentBuilder()
        {
            _authorizePayment = new AuthorizePayment();
        }

        public AuthorizePaymentBuilder AddRunEnvironment(AuthorizeNet.Environment environment)
        {
            ApiOperationBase<ANetApiRequest,
                ANetApiResponse>.RunEnvironment = environment;
            return this;
        }

        public AuthorizePaymentBuilder AddMerchantAuthentication(
            merchantAuthenticationType merchantAuthentication)
        {
            ApiOperationBase<ANetApiRequest,
                ANetApiResponse>.MerchantAuthentication = merchantAuthentication;
            return this;
        }

        public AuthorizePaymentBuilder AddTransactionController(transactionRequestType requestType)
        {
            var request = new createTransactionRequest()
            {
                transactionRequest = requestType
            };

            var controller = new createTransactionController(request);
            _authorizePayment.Controller = controller;

            return this;
        }

        public AuthorizePayment Build()
        {
            return _authorizePayment;
        }
    }
}