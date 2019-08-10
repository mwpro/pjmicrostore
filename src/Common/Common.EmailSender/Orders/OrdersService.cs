using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Orders.Contracts.ApiModels;
using Common.Infrastructure;
using Flurl.Http;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;

namespace Common.EmailSender.Orders
{
    public interface IOrdersService
    {
        Task<OrderDetails> GetOrder(int orderId);
    }

    public class OrdersService : IOrdersService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthorizationTokenService _authorizationTokenService;

        public OrdersService(IConfiguration configuration, IAuthorizationTokenService authorizationTokenService)
        {
            _configuration = configuration;
            _authorizationTokenService = authorizationTokenService;
        }

        public async Task<OrderDetails> GetOrder(int orderId)
        {
            var order = await $"{_configuration.GetValue<string>("Dependencies:Orders")}/api/"
                .WithOAuthBearerToken(await _authorizationTokenService.GetBearerToken())
                .AppendPathSegments("orders", orderId)
                .GetJsonAsync<OrderDetails>();

            return order;
        }
    }
}
