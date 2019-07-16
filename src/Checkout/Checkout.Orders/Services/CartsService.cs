using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Cart.Contracts.ApiModels;
using Flurl;
using Flurl.Http;
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
                .AppendPathSegments("cart", cartId)
                .GetJsonAsync<CartDto>();

            return cart;
        }
    }
}
