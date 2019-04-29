using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Checkout.Orders.Services
{
    public interface ICartsService
    {
        Task<Cart> GetCart(int cartId);
    }

    public class CartsService : ICartsService
    {
        private readonly IConfiguration _configuration;

        public CartsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Cart> GetCart(int cartId)
        {
            var cart = await $"{_configuration.GetValue<string>("Dependencies:Cart")}/api/"
                .AppendPathSegments("cart", cartId)
                .GetJsonAsync<Cart>();

            return cart;
        }
    }


    public class Cart
    {
        public int CartId { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
        public decimal Total { get; set; }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Value { get; set; }
    }
}
