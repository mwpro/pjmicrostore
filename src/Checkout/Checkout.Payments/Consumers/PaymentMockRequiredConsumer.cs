using System;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Payments.Contracts.Events;
using Checkout.Payments.Domain;
using MassTransit;

namespace Checkout.Payments.Consumers
{
    public class PaymentMockRequiredConsumer : IConsumer<PaymentMockRequired>
    {
        private readonly PaymentsDbContext _paymentsDbContext;

        public PaymentMockRequiredConsumer(PaymentsDbContext paymentsDbContext)
        {
            _paymentsDbContext = paymentsDbContext;
        }

        public async Task Consume(ConsumeContext<PaymentMockRequired> context)
        {
            Thread.Sleep(5000 + context.SentTime.Value.Second); // todo "random" waiting time
            _paymentsDbContext.MockPayments.Add(new MockPayement()
            {
                Amount = context.Message.Amount,
                PaymentReference = context.Message.PaymentReference,
                PaymentDescription = context.Message.Description,
                PaymentStatus = PaymentStatus.New,
                ProviderReference = Guid.NewGuid()
            });
            await _paymentsDbContext.SaveChangesAsync();
        }
    }
}