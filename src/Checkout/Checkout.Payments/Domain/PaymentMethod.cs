using System.Collections.Generic;

namespace Checkout.Payments.Contracts
{
    public class PaymentMethod
    {
        public static readonly PaymentMethod OnDelivery = new PaymentMethod()
        {
            Name = PaymentMethodNames.OnDelivery,
            SupportedDeliveryMethods = new []
            {
                new SupportedDeliveryMethod() { DeliveryMethodName = "StorePickup", PaymentFee = 0 },
                new SupportedDeliveryMethod() { DeliveryMethodName = "Courier", PaymentFee = 7.50m },
            }
        };
        public static readonly PaymentMethod PaymentProvider = new PaymentMethod()
        {
            Name = PaymentMethodNames.PaymentProvider,
            SupportedDeliveryMethods = new[]
            {
                new SupportedDeliveryMethod() { DeliveryMethodName = "StorePickup", PaymentFee = 0 },
                new SupportedDeliveryMethod() { DeliveryMethodName = "Courier", PaymentFee = 0 },
                new SupportedDeliveryMethod() { DeliveryMethodName = "Post", PaymentFee = 0 },
            }
        };

        public static readonly IEnumerable<PaymentMethod> GetAll = new[]
        {
            OnDelivery,
            PaymentProvider
        };

        public string Name { get; set; }
        public IEnumerable<SupportedDeliveryMethod> SupportedDeliveryMethods { get; set; }
    }

    public class SupportedDeliveryMethod
    {
        public string DeliveryMethodName { get; set; }
        public decimal PaymentFee { get; set; }
    }
}