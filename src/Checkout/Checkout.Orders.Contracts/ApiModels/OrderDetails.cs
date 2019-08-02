using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Orders.Contracts.ApiModels
{
    public class OrderDetails
    {
        public OrderDetails()
        {
            OrderLines = new List<OrderDetailsLine>();
        }

        public int Id { get; set; }
        public DateTime CreateDate { get; set; } // todo rename to createDate utc
        public string Status { get; set; }

        public decimal Total => OrderLines.Sum(x => x.Value);

        public IList<OrderDetailsLine> OrderLines { get; set; }
        public OrderDetailsCustomer Customer { get; set; }
        public OrderDetailsAddress BillingAddress { get; set; }
        public OrderDetailsAddress ShippingAddress { get; set; }

        public class OrderDetailsAddress
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Zip { get; set; }
        }

        public class OrderDetailsLine
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal ProductPrice { get; set; }
            public int Quantity { get; set; }
            public decimal Value => ProductPrice * Quantity;
        }

        public class OrderDetailsCustomer
        {
            public Guid? CustomerId { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }
    }
}
