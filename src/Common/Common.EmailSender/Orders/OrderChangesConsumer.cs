using System;
using System.Threading.Tasks;
using Checkout.Orders.Contracts.Events;
using Common.EmailSender.Infrastructure;
using MassTransit;

namespace Common.EmailSender.Orders
{
    public class OrderChangesConsumer : IConsumer<OrderPlacedEvent>, 
                                        IConsumer<OrderCanceledEvent>,
                                        IConsumer<OrderCompletedEvent>,
                                        IConsumer<OrderWaitsForShipmentEvent>
    {
        private readonly IOrdersService _ordersService;
        private readonly ISendMailService _sendMailService;

        public OrderChangesConsumer(IOrdersService ordersService, ISendMailService sendMailService)
        {
            _ordersService = ordersService;
            _sendMailService = sendMailService;
        }

        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            Console.WriteLine($"order placed {context.Message.OrderId}");
            var order = await _ordersService.GetOrder(context.Message.OrderId);

            await _sendMailService.SendMail(order.Customer.Email, $"Zamówienie {order.Id} przyjęte", "OrderPlacedEmail", order);
        }

        public async Task Consume(ConsumeContext<OrderCanceledEvent> context)
        {
            Console.WriteLine($"order completed {context.Message.OrderId}");
            var order = await _ordersService.GetOrder(context.Message.OrderId);

            await _sendMailService.SendMail(order.Customer.Email, $"Zamówienie {order.Id} zostało anulowane", "OrderCanceledEmail", order);
        }

        public async Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            Console.WriteLine($"order canceled {context.Message.OrderId}");
            var order = await _ordersService.GetOrder(context.Message.OrderId);

            await _sendMailService.SendMail(order.Customer.Email, $"Zamówienie {order.Id} zostało zrealizowane", "OrderCompletedEmail", order);
        }

        public async Task Consume(ConsumeContext<OrderWaitsForShipmentEvent> context)
        {
            Console.WriteLine($"order waits for shipment {context.Message.OrderId}");
            var order = await _ordersService.GetOrder(context.Message.OrderId);

            await _sendMailService.SendMail(order.Customer.Email, $"Zamówienie {order.Id} oczekuje na wysyłkę", "OrderWaitsForShipmentEmail", order);
        }
    }
}