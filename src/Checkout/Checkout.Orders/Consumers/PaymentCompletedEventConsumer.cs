using System.Threading.Tasks;
using Checkout.Orders.Commands;
using Checkout.Payments.Contracts.Events;
using MassTransit;
using MediatR;

namespace Checkout.Orders.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly IMediator _mediator;

        public PaymentCompletedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            await _mediator.Send(new MakeOrderWaitForShippingCommand(context.Message.OrderId));
        }
    }
}
