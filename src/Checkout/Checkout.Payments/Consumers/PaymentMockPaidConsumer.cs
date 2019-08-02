using System.Linq;
using System.Threading.Tasks;
using Checkout.Payments.Contracts.Events;
using Checkout.Payments.Domain;
using MassTransit;

namespace Checkout.Payments.Consumers
{
    public class PaymentMockPaidConsumer : IConsumer<PaymentMockPaid>
    {
        private readonly PaymentsDbContext _paymentsDbContext;

        public PaymentMockPaidConsumer(PaymentsDbContext paymentsDbContext)
        {
            _paymentsDbContext = paymentsDbContext;
        }

        public async Task Consume(ConsumeContext<PaymentMockPaid> context)
        {
            var payment = _paymentsDbContext.Payments.FirstOrDefault(x => x.PaymentReference == context.Message.PaymentReference);

            payment.PaymentStatus = PaymentStatus.Completed;

            await context.Publish(new PaymentCompletedEvent()
            {
                OrderId = payment.OrderId,
                PaymentId = payment.Id,
                PaymentReference = payment.PaymentReference
            });

            await _paymentsDbContext.SaveChangesAsync();
        }
    }
}