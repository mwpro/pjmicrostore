using System;
using System.IO;
using System.Threading.Tasks;
using Checkout.Orders.Contracts.Events;
using FluentEmail.Core;
using FluentEmail.Razor;
using MassTransit;

namespace Common.EmailSender.Consumers
{
    public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
    {
        public OrderPlacedConsumer()
        {
        }

        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            Console.WriteLine($"order placed {context.Message.OrderId}");

            var email = Email
                .From("sener@test.dev")
                .To("test@test.dev")
                .Subject("woo nuget")
                .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/Templates/OrderPlacedEmail.cshtml", context.Message)
                .Send();

            throw new Exception();
        }
    }
}