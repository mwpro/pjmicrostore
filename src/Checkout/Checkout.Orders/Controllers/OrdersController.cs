using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Checkout.Orders.Commands;
using Checkout.Orders.Contracts.Events;
using Checkout.Orders.Domain;
using Checkout.Orders.Queries;
using Checkout.Orders.Services;
using Checkout.Payments.Contracts;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Orders.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private const int CartIdMock = 1; // todo what next?
        private const int CustomerIdMock = 1; // todo what next?
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> PlaceOrder(PlaceOrderModel placeOrderModel)
        {
            var createdOrder = await _mediator.Send(new PlaceOrderCommand(CartIdMock, placeOrderModel.PaymentMethod, placeOrderModel.Email,
                placeOrderModel.ShippingDetails.ToOrderAddress(), placeOrderModel.BillingDetails.ToOrderAddress(), CustomerIdMock, placeOrderModel.Phone));

            if (placeOrderModel.PaymentMethod != PaymentMethods.OnDelivery)
            {
                return StatusCode((int)HttpStatusCode.Created, new
                {// todo some model?
                    PaymentCheckUrl = $"/api/payments/{createdOrder.PaymentReference}"
                });
            }

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPost("{orderId}/sent")]
        public async Task<IActionResult> MarkAsSent(int orderId) // todo temporary endpoint until we have shipment microservice
        {
            var result = await _mediator.Send(new MarkOrderAsCompletedCommand(orderId));

            return Ok(result);
        }


        [HttpPost("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId) 
        {
            var result = await _mediator.Send(new CancelOrderCommand(orderId)); // todo reason

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _mediator.Send(new GetOrderListQuery());

            return Ok(result);
        }


        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var result = await _mediator.Send(new GetOrderDetailsQuery(orderId));

            return Ok(result);
        }
    }

    public class PlaceOrderModel
    {
        public string PaymentMethod { get; set; } // todo method id or code?

        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressModel ShippingDetails { get; set; }
        public AddressModel BillingDetails { get; set; }

        public class AddressModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Zip { get; set; }

            public PlaceOrderCommand.OrderAddress ToOrderAddress()
            {
                return new PlaceOrderCommand.OrderAddress(FirstName, LastName, Address, City, Zip);
            }

        }
    }
}
