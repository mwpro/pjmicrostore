using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
