namespace Checkout.Orders.Domain
{
    public class OrderLine
    {
        public OrderLine(int productId, string productName, decimal productPrice, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
        }

        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal Value => ProductPrice * Quantity;
    }
}