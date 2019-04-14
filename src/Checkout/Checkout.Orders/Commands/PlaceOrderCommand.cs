using System;
using MediatR;

namespace Checkout.Orders.Commands
{
    public class PlaceOrderCommand : IRequest<PlaceOrderCommandResponse>
    {
        public PlaceOrderCommand(int cartId, string paymentMethod, string email, OrderAddress shippingDetails, OrderAddress billingDetails, int customerId, string phone)
        {
            CartId = cartId;
            PaymentMethod = paymentMethod;
            Email = email;
            ShippingDetails = shippingDetails;
            BillingDetails = billingDetails;
            CustomerId = customerId;
            Phone = phone;
        }

        public int CartId { get; }
        public int CustomerId { get; }

        public string PaymentMethod { get; } // todo method id or code?

        public string Email { get; }
        public string Phone { get; }

        public OrderAddress ShippingDetails { get; }
        public OrderAddress BillingDetails { get; }

        public class OrderAddress    
        {
            public OrderAddress(string firstName, string lastName, string address, string city, string zip)
            {
                FirstName = firstName;
                LastName = lastName;
                Address = address;
                City = city;
                Zip = zip;
            }

            public string FirstName { get; }
            public string LastName { get; }
            public string Address { get; }
            public string City { get; }
            public string Zip { get; }
        }
    }

    public class PlaceOrderCommandResponse : IRequest
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public Guid? PaymentReference { get; set; }
    }
}
