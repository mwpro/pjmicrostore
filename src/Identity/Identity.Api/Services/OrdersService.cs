using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Orders.Contracts.ApiModels;
using Common.Infrastructure;
using Flurl.Http;
using IdentityModel.Client;
using IdentityServer4;
using Microsoft.Extensions.Configuration;

namespace Identity.Api.Services
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
