using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

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
