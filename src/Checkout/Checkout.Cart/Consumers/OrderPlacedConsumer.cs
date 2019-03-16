using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Cart.Domain;
using Checkout.Orders.Controllers;
using MassTransit;

namespace Checkout.Orders.Controllers
{
    public class OrderPlacedEvent
    {
        public int OrderId { get; set; }
        public int SourceCartId { get; set; }
    }
}

namespace Checkout.Cart.Consumers
{
    public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
    {
        private readonly CartContext _cartContext;

        public OrderPlacedConsumer(CartContext cartContext)
        {
            _cartContext = cartContext;
        }

        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            var cart = _cartContext.Carts.FirstOrDefault(x => x.Id == context.Message.SourceCartId);
            if (cart == null)
            {
                throw new Exception($"Could not find cart with id {context.Message.SourceCartId}");
            }

            _cartContext.Carts.Remove(cart);
            await _cartContext.SaveChangesAsync();
        }
    }
}
