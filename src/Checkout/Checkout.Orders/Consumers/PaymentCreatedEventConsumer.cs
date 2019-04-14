using System.Threading.Tasks;
using Checkout.Orders.Commands;
using Checkout.Payments.Contracts.Events;
using MassTransit;
using MediatR;

namespace Checkout.Orders.Consumers
{
    public class PaymentCreatedEventConsumer : IConsumer<PaymentCreatedEvent>
    {
        private readonly IMediator _mediator;

        public PaymentCreatedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<PaymentCreatedEvent> context)
        {
            await _mediator.Send(new MakeOrderWaitForPaymentCommand(context.Message.OrderId));
        }
    }
}