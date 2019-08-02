using System;

namespace Checkout.Orders.Domain
{
    public class OrderStatus : IEquatable<OrderStatus>
    {
        public static OrderStatus New = new OrderStatus("New");
        public static OrderStatus WaitingForPayment = new OrderStatus("WaitingForPayment");
        public static OrderStatus WaitingForShipment = new OrderStatus("WaitingForShipment");
        public static OrderStatus Completed = new OrderStatus("Completed");
        public static OrderStatus Cancelled = new OrderStatus("Cancelled");

        public string Name { get; private set; }

        private OrderStatus(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(OrderStatus other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }
    }
}