namespace Checkout.Orders.Contracts.Events
{
    public class OrderPlacedEvent
    {
        public int OrderId { get; set; }
        public int SourceCartId { get; set; }
    }
}
