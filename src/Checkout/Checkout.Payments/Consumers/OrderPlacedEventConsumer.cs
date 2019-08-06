using System.Threading.Tasks;
using Checkout.Orders.Contracts.Events;
using Checkout.Payments.Contracts;
using Checkout.Payments.Contracts.Events;
using Checkout.Payments.Domain;
using MassTransit;

namespace Checkout.Payments.Consumers
{
    public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
    {
        private readonly PaymentsDbContext _paymentsDbContext;

        public OrderPlacedEventConsumer(PaymentsDbContext paymentsDbContext)
        {
            _paymentsDbContext = paymentsDbContext;
        }

        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            var payment = new Payment()
            {
                Amount = context.Message.Amount,
                OrderId = context.Message.OrderId,
                PaymentMethod = context.Message.PaymentMethod,
                PaymentStatus = PaymentStatus.New,
                PaymentReference = context.Message.PaymentReference
            };

            _paymentsDbContext.Payments.Add(payment);
            await _paymentsDbContext.SaveChangesAsync();

            await context.Publish(new PaymentCreatedEvent()
            {
                PaymentId = payment.Id,
                PaymentReference = payment.PaymentReference,
                OrderId = payment.OrderId,
            });

            // todo tempoary, should start correct saga here...
            if (context.Message.PaymentMethod == PaymentMethod.OnDelivery.Name)
            {
                payment.PaymentStatus = PaymentStatus.Completed;

                await _paymentsDbContext.SaveChangesAsync();

                await context.Publish(new PaymentCompletedEvent() // todo sending PaymentCompleted for order to be paid on delivery is a lie...
                {
                    OrderId = payment.OrderId,
                    PaymentId = payment.Id,
                    PaymentReference = payment.PaymentReference
                });
            }

            if (context.Message.PaymentMethod == PaymentMethod.PaymentProvider.Name)
            {
                await context.Publish(new PaymentMockRequired()
                {
                    Amount = payment.Amount,
                    Description = $"Płatność za zamówienie {payment.OrderId} dla pjmicrostore",
                    PaymentReference = payment.PaymentReference
                });
            }
        }
    }
}
