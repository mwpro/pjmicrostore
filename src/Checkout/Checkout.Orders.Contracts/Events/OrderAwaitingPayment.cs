using System;

namespace Checkout.Orders.Contracts.Events
{
    public class OrderAwaitingPayment
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public Guid PaymentReference { get; set; } // todo something is meh 'bout the payment reference...
    }
}