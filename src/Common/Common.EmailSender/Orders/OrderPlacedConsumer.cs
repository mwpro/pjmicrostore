using System;
using System.Threading.Tasks;
using Checkout.Orders.Contracts.Events;
using Common.EmailSender.Infrastructure;
using MassTransit;

namespace Common.EmailSender.Orders
{
    public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
    {
        private readonly IOrdersService _ordersService;
        private readonly ISendMailService _sendMailService;

        public OrderPlacedConsumer(IOrdersService ordersService, ISendMailService sendMailService)
        {
            _ordersService = ordersService;
            _sendMailService = sendMailService;
        }

        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            Console.WriteLine($"order placed {context.Message.OrderId}");
            var order = await _ordersService.GetOrder(context.Message.OrderId);

            await _sendMailService.SendMail(order.Customer.Email, $"Zamówienie {order.Id}", "OrderPlacedEmail", order);
        }
    }
}