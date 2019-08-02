using System;
using System.Collections.Generic;

namespace Checkout.Cart.Contracts.ApiModels
{
    public class CartDto
    {
        public int CartId { get; set; }
        public Guid? CartAccessToken { get; set; }
        public IEnumerable<CartItemDto> CartItems { get; set; }
        public decimal Total { get; set; }
        public int NumberOfItems { get; set; }

        public class CartItemDto
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal ProductPrice { get; set; }
            public decimal Value { get; set; }
        }
    }
}
