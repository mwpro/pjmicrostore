namespace Checkout.Orders.Domain
{
    public class Customer
    {
        public Customer(int customerId, string email, string phone)
        {
            CustomerId = customerId;
            Email = email;
            Phone = phone;
        }
        
        public int CustomerId { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
    }
}