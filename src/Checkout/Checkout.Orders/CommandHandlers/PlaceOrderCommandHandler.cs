using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Commands;
using Checkout.Orders.Contracts.Events;
using Checkout.Orders.Domain;
using Checkout.Orders.Services;
using Checkout.Payments.Contracts;
using MassTransit;
using MediatR;

namespace Checkout.Orders.CommandHandlers
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, PlaceOrderCommandResponse>
    {
        private readonly ICartsService _cartsService;
        private readonly OrdersContext _ordersContext;
        private readonly IBus _bus;

        public PlaceOrderCommandHandler(ICartsService cartsService, OrdersContext ordersContext, IBus bus)
        {
            _cartsService = cartsService;
            _ordersContext = ordersContext;
            _bus = bus;
        }

        public async Task<PlaceOrderCommandResponse> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartsService.GetCart(request.CartId);

            var order = new Order(
                new Customer("Jan", "Kowalski","jan@kowalski.pl","123-123-123"),
                cart.CartItems.Select(x => new OrderLine(x.ProductId, x.ProductName, x.ProductPrice, x.Quantity)).ToList()
            );

            _ordersContext.Add(order);
            _ordersContext.SaveChanges();

            await _bus.Publish(new OrderPlacedEvent()
            {
                OrderId = order.Id,
                SourceCartId = cart.CartId
            });

            Guid? paymentReference = null;
            if (request.PaymentMethod != PaymentMethods.OnDelivery) // todo should not be handled here9
            {
                paymentReference = NewId.NextGuid();
                await _bus.Publish(new OrderAwaitsPayment()
                {
                    OrderId = order.Id,
                    Amount = order.Total,
                    PaymentMethod = request.PaymentMethod,
                    PaymentReference = paymentReference.Value
                });
            }

            return new PlaceOrderCommandResponse()
            {
                OrderId = order.Id,
                Amount = order.Total,
                PaymentReference = paymentReference
            };
        }
    }
}
