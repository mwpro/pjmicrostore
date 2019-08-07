using System.Threading.Tasks;
using Checkout.Cart.Contracts.ApiModels;
using Checkout.Payments.Contracts;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Checkout.Orders.Services
{
    public interface IPaymentService
    {
        Task<PaymentMethodDto> GetPaymentMethod(string paymentMethodName, string deliveryMethodName);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;

        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PaymentMethodDto> GetPaymentMethod(string paymentMethodName, string deliveryMethodName)
        {
            var paymentMethod = await $"{_configuration.GetValue<string>("Dependencies:Payment")}/api/"
                .AppendPathSegments("payments", "methods", paymentMethodName)
                .SetQueryParam("deliveryMethod", deliveryMethodName)
                .GetJsonAsync<PaymentMethodDto>();

            return paymentMethod;
        }
    }
}