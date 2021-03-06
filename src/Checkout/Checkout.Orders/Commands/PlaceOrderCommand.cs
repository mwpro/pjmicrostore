﻿using System;
using MediatR;

namespace Checkout.Orders.Commands
{
    public class PlaceOrderCommand : IRequest<PlaceOrderCommandResponse>
    {
        public PlaceOrderCommand(Guid cartAccessToken, string deliveryMethod, string paymentMethod, string email, OrderAddress shippingDetails, 
            OrderAddress billingDetails, Guid? customerId, string phone)
        {
            CartAccessToken = cartAccessToken;
            DeliveryMethod = deliveryMethod;
            PaymentMethod = paymentMethod;
            Email = email;
            ShippingDetails = shippingDetails;
            BillingDetails = billingDetails;
            CustomerId = customerId;
            Phone = phone;
        }

        public Guid CartAccessToken { get; }
        public Guid? CustomerId { get; }

        public string DeliveryMethod { get; }
        public string PaymentMethod { get; }

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
