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
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // TODO place order
        [HttpPost("")]
        public async Task<IActionResult> PlaceOrder(PlaceOrderModel placeOrderModel)
        {
            var createdOrder = await _mediator.Send(new PlaceOrderCommand(CartIdMock, placeOrderModel.PaymentMethod));
            if (placeOrderModel.PaymentMethod != PaymentMethods.OnDelivery)
            {
                return StatusCode((int)HttpStatusCode.Created, new
                {// todo some model?
                    PaymentCheckUrl = $"/api/payments/{createdOrder.PaymentReference}"
                });
            }

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _mediator.Send(new GetOrderListQuery());

            return Ok(result);
        }
    }

    public class PlaceOrderModel
    {
        public string PaymentMethod { get; set; } // todo method id or code?
    }
}
