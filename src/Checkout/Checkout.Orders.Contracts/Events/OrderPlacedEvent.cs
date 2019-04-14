using System;

namespace Checkout.Orders.Contracts.Events
{
    public class OrderPlacedEvent
    {
        public int OrderId { get; set; }
        public int SourceCartId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public Guid PaymentReference { get; set; }
    }
}
