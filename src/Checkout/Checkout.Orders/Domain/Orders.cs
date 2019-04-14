using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Checkout.Orders.Domain
{
    public class Order
    {
        public Order(Customer customer, ICollection<OrderLine> orderLines)
        {
            CreateDate = DateTime.UtcNow;
            Status = "New";
            OrderLines = orderLines;
            Customer = customer;
        }

        public Order(int id, DateTime createDate, string status, int customerId)
        {
            Id = id;
            CreateDate = createDate;
            Status = status;
            CustomerId = customerId;
        }

        public int Id { get; private set; }
        // todo fancy order number like 1234/07/2018
        public DateTime CreateDate { get; private set; } // todo rename to createDate utc
        public string Status { get; private set; }
        public ICollection<OrderLine> OrderLines { get; private set; }

        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }

        public decimal Total => OrderLines.Sum(x => x.Value);
    }

    public class Customer
    {
        public Customer(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
    }

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
