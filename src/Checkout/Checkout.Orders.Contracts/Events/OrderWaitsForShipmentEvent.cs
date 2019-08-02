namespace Checkout.Orders.Contracts.Events
{
    public class OrderWaitsForShipmentEvent
    {
        public int OrderId { get; set; }
    }
}