using System.Collections.Generic;

namespace Checkout.Payments.Contracts
{
    public static class PaymentMethods
    {
        public static readonly IEnumerable<string> GetAll = new[]
        {
            OnDelivery,
            PaymentProvider
        };

        public const string OnDelivery = "OnDelivery";
        public const string PaymentProvider = "PaymentProvider";
    }
}
