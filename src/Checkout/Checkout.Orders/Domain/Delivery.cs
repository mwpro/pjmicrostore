namespace Checkout.Orders.Domain
{
    public class Delivery
    {
        public Delivery(string name, decimal fee)
        {
            Name = name;
            Fee = fee;
        }

        public string Name { get; private set; }
        public decimal Fee { get; private set; }
    }
}