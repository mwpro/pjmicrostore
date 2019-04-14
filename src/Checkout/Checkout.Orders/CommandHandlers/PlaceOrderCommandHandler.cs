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
                new Customer(request.CustomerId, request.Email, request.Phone), 
                cart.CartItems.Select(x => new OrderLine(x.ProductId, x.ProductName, x.ProductPrice, x.Quantity)).ToList(),
                new CustomerAddress(request.BillingDetails.FirstName, request.BillingDetails.LastName,
                    request.BillingDetails.Address, request.BillingDetails.City, request.BillingDetails.Zip),
                new CustomerAddress(request.ShippingDetails.FirstName, request.ShippingDetails.LastName,
                    request.ShippingDetails.Address, request.ShippingDetails.City, request.ShippingDetails.Zip)
            );

            _ordersContext.Add(order);
            _ordersContext.SaveChanges();


            // todo should not be handled here.
            Guid? paymentReference = NewId.NextGuid();
            await _bus.Publish(new OrderPlacedEvent()
            {
                OrderId = order.Id,
                SourceCartId = cart.CartId,
                Amount = order.Total,
                PaymentMethod = request.PaymentMethod,
                PaymentReference = paymentReference.Value
            });

            return new PlaceOrderCommandResponse()
            {
                OrderId = order.Id,
                Amount = order.Total,
                PaymentReference = paymentReference
            };
        }
    }
}
