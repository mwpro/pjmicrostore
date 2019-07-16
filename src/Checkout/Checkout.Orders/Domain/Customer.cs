using System;

namespace Checkout.Orders.Domain
{
    public class Customer
    {
        public Customer(Guid? customerId, string email, string phone)
        {
            CustomerId = customerId;
            Email = email;
            Phone = phone;
        }
        
        public Guid? CustomerId { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
    }
}