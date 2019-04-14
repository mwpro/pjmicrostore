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
            Status = OrderStatus.New;
            OrderLines = orderLines;
            Customer = customer;
        }

        public Order(int id, DateTime createDate, int customerId)
        {
            Id = id;
            CreateDate = createDate;
            CustomerId = customerId;
        }

        public int Id { get; private set; }
        // todo fancy order number like 1234/07/2018
        public DateTime CreateDate { get; private set; } // todo rename to createDate utc
        public OrderStatus Status { get; private set; }
        public ICollection<OrderLine> OrderLines { get; private set; }

        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }

        public decimal Total => OrderLines.Sum(x => x.Value);

        public void MarkAsPaid()
        {
            if (!Status.Equals(OrderStatus.WaitingForPayment)) throw new Exception($"Cannot make order in status {Status} as {OrderStatus.WaitingForShipment}"); // todo custom exception type

            Status = OrderStatus.WaitingForShipment;
        }

        public void MarkAsWaitingForPayment()
        {
            if (!Status.Equals(OrderStatus.New)) throw new Exception($"Cannot make order in status {Status} as {OrderStatus.WaitingForPayment}"); // todo custom exception type

            Status = OrderStatus.WaitingForPayment;
        }
    }
}
