using System;

namespace Checkout.Payments.Contracts.Events
{
    public class PaymentMockRequired // todo that's command..
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Guid PaymentReference { get; set; }
    }

    public class PaymentMockPaid // todo that's command..
    {
        public int PaymentMockId { get; set; }
        public Guid PaymentReference { get; set; }
    }

    public class PaymentCreatedEvent
    {
        public int PaymentId { get; set; }
        public Guid PaymentReference { get; set; }
        public int OrderId { get; set; }
    }

    public class PaymentCompletedEvent
    {
        public int PaymentId { get; set; }
        public Guid PaymentReference { get; set; }
        public int OrderId { get; set; }
    }
}
