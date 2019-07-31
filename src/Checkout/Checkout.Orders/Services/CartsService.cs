using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Cart.Contracts.ApiModels;
using Flurl;
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

        public CartsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CartDto> GetCart(Guid cartId)
        {
            var cart = await $"{_configuration.GetValue<string>("Dependencies:Cart")}/api/"
                .WithOAuthBearerToken(await GetBearerToken())
                .AppendPathSegments("cart", cartId)
                .GetJsonAsync<CartDto>();

            return cart;
        }

        private async Task<string> GetBearerToken()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                throw disco.Exception;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "orders",
                ClientSecret = "ordersSecret"
            });

            if (tokenResponse.IsError)
            {
                throw tokenResponse.Exception;
            }

            return tokenResponse.AccessToken;
        }
    }
}
