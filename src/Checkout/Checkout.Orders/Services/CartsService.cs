using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Cart.Contracts.ApiModels;
using Common.Infrastructure;
using Flurl.Http;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;

namespace Checkout.Orders.Services
{
    public interface ICartsService
    {
        Task<CartDto> GetCart(Guid cartId);
    }

    public class CartsService : ICartsService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthorizationTokenService _authorizationTokenService;

        public CartsService(IConfiguration configuration, IAuthorizationTokenService authorizationTokenService)
        {
            _configuration = configuration;
            _authorizationTokenService = authorizationTokenService;
        }

        public async Task<CartDto> GetCart(Guid cartId)
        {
            var cart = await $"{_configuration.GetValue<string>("Dependencies:Cart")}/api/"
                .WithOAuthBearerToken(await _authorizationTokenService.GetBearerToken())
                .AppendPathSegments("cart", cartId)
                .GetJsonAsync<CartDto>();

            return cart;
        }
    }
}
