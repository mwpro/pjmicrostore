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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Orders.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> PlaceOrder(PlaceOrderModel placeOrderModel)
        {
            // todo validation
            var createdOrder = await _mediator.Send(new PlaceOrderCommand(placeOrderModel.CartAccessToken, placeOrderModel.PaymentMethod, placeOrderModel.Email,
                placeOrderModel.ShippingDetails.ToOrderAddress(), placeOrderModel.BillingDetails.ToOrderAddress(), GetUserId(), placeOrderModel.Phone));

            // todo this if does not look good here,
            // i think we should move it all to payments service and just return action link here
            // cause probably more async actions may happen in background - like stock ensuring
            if (placeOrderModel.PaymentMethod != PaymentMethods.OnDelivery)
            {
                return StatusCode((int)HttpStatusCode.Created, new
                {
                    PaymentCheckUrl = $"/api/payments/{createdOrder.PaymentReference}"
                });
            }

            return StatusCode((int)HttpStatusCode.Created);
        }

        [Authorize(AuthorizationPolicies.AdminOnly)]
        [HttpPost("{orderId}/sent")]
        public async Task<IActionResult> MarkAsSent(int orderId) // todo temporary endpoint until we have shipment microservice
        {
            var result = await _mediator.Send(new MarkOrderAsCompletedCommand(orderId));

            return Ok(result);
        }

        [Authorize(AuthorizationPolicies.AdminOnly)]
        [HttpPost("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId) 
        {
            var result = await _mediator.Send(new CancelOrderCommand(orderId)); // todo reason

            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            if (User.IsInRole(Roles.Admin))
            {
                return Ok(await _mediator.Send(new GetOrderListQuery()));
            }
            
            var userId = GetUserId();
            return Ok(await _mediator.Send(new GetUserOrderListQuery(userId.Value)));
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            OrderDetails result;
            if (User.IsInRole(Roles.Admin))
            {
                result = await _mediator.Send(new GetOrderDetailsQuery(orderId));
            }
            else
            {
                var userId = GetUserId();
                result = await _mediator.Send(new GetUserOrderDetailsQuery(orderId, userId.Value));
            }

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        private Guid? GetUserId()
        {
            var claimValue = User.Claims.FirstOrDefault(x => x.Type == "sub");
            if (claimValue == null || string.IsNullOrWhiteSpace(claimValue.Value)
                || !Guid.TryParse(claimValue.Value, out var userId))
            {
                return null;
            }

            return userId;
        }
    }

    public class PlaceOrderModel
    {
        public string PaymentMethod { get; set; } // todo method id or code?

        public Guid CartAccessToken { get; set; }

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
