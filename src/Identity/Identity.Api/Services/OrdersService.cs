﻿using System.Net.Http;
using System.Threading.Tasks;
using Checkout.Orders.Contracts.ApiModels;
using Flurl.Http;
using IdentityModel.Client;
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

        public OrdersService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<OrderDetails> GetOrder(int orderId)
        {
            var order = await $"{_configuration.GetValue<string>("Dependencies:Orders")}/api/"
                .WithOAuthBearerToken(await GetBearerToken())
                .AppendPathSegments("orders", orderId)
                .GetJsonAsync<OrderDetails>();

            return order;
        }

        private async Task<string> GetBearerToken() // todo as we are in IdenityApi - maybe we could do it locally?
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
                ClientId = "identityApi",
                ClientSecret = "identityApiSecret"
            });

            if (tokenResponse.IsError)
            {
                throw tokenResponse.Exception;
            }

            return tokenResponse.AccessToken;
        }
    }
}