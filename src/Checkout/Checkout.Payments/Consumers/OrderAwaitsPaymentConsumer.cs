using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Orders.Contracts.Events;
using Checkout.Payments.Contracts.Events;
using Checkout.Payments.Domain;
using MassTransit;

namespace Checkout.Payments.Consumers
{
    public class OrderAwaitsPaymentConsumer : IConsumer<OrderAwaitsPayment>
    {
        private readonly PaymentsDbContext _paymentsDbContext;

        public OrderAwaitsPaymentConsumer(PaymentsDbContext paymentsDbContext)
        {
            _paymentsDbContext = paymentsDbContext;
        }

        public async Task Consume(ConsumeContext<OrderAwaitsPayment> context)
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
            await context.Publish(new PaymentMockRequired()
            {
                Amount = payment.Amount,
                Description = $"Płatność za zamówienie {payment.OrderId} dla pjmicrostore",
                PaymentReference = payment.PaymentReference
            });
        }
    }
}
