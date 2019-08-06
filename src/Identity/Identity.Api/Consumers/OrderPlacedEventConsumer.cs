using System;
using System.Threading.Tasks;
using Checkout.Orders.Contracts.Events;
using Identity.Api.Models;
using Identity.Api.Services;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Identity.Api.Consumers
{
    public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrdersService _ordersService;
        private readonly ILogger<OrderPlacedEventConsumer> _logger;

        public OrderPlacedEventConsumer(UserManager<ApplicationUser> userManager, ILogger<OrderPlacedEventConsumer> logger, IOrdersService ordersService)
        {
            _userManager = userManager;
            _logger = logger;
            _ordersService = ordersService;
        }

        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            if (!context.Message.CustomerId.HasValue)
            {
                _logger.LogInformation($"Skipping user update for order {context.Message.OrderId} - it was a guest's order");
                return;
            }

            var user = await _userManager.FindByIdAsync(context.Message.CustomerId.ToString());
            if (user == null)
                throw new Exception($"User {context.Message.CustomerId} from order {context.Message.OrderId} not found");

            var orderDetails = await _ordersService.GetOrder(context.Message.OrderId);
            if (orderDetails == null)
                throw new Exception($"Order {context.Message.OrderId} not found");

            user.PhoneNumber = orderDetails.Customer.Phone;
            user.ShippingAddress = new ApplicationUser.Address(orderDetails.ShippingAddress.FirstName,
                orderDetails.ShippingAddress.LastName, orderDetails.ShippingAddress.Address,
                orderDetails.ShippingAddress.City, orderDetails.ShippingAddress.Zip);

            // if shipping and billing addresses are the same, mark billing as empty so user won't be confused at the next order
            // todo dummy copying of shipping to billing looks not smart right here...
            if (orderDetails.ShippingAddress.FirstName == orderDetails.BillingAddress.FirstName &&
                orderDetails.ShippingAddress.LastName == orderDetails.BillingAddress.LastName &&
                orderDetails.ShippingAddress.Address == orderDetails.BillingAddress.Address &&
                orderDetails.ShippingAddress.City == orderDetails.BillingAddress.City &&
                orderDetails.ShippingAddress.Zip == orderDetails.BillingAddress.Zip)
            {
                user.BillingAddress = ApplicationUser.Address.Empty();
            }
            else
            {
                user.BillingAddress = new ApplicationUser.Address(orderDetails.BillingAddress.FirstName,
                    orderDetails.BillingAddress.LastName, orderDetails.BillingAddress.Address,
                    orderDetails.BillingAddress.City, orderDetails.BillingAddress.Zip);
            }

            await _userManager.UpdateAsync(user);
        }
    }
}
