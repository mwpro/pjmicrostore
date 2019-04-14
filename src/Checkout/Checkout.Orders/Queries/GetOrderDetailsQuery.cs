using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Domain;
using Checkout.Orders.Infrastructure;
using Dapper;
using MassTransit;
using MediatR;

namespace Checkout.Orders.Queries
{
    public class GetOrderDetailsQuery : IRequest<OrderDetails>
    {
        public GetOrderDetailsQuery(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }

    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetails>
    {
        private readonly IDatabase _database;

        private const string GetOrderQuerySql = @"
SELECT 
    o.[Id],
    o.[CreateDate],
    o.[Status],
    o.[CustomerId],
    c.[FirstName],
    c.[LastName],
    c.[Email],
    c.[Phone],
    ol.[ProductId],
    ol.[ProductName],
    ol.[ProductPrice],
    ol.[Quantity],
    ol.[OrderId]
FROM .[dbo].[Order] o
LEFT JOIN [dbo].[OrderLines] ol ON o.Id = ol.OrderId
LEFT JOIN [dbo].[Customer] c ON o.CustomerId = c.Id
WHERE o.Id = @orderId";

        public GetOrderDetailsQueryHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<OrderDetails> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _database.ExecuteOnConnection(async connection =>
            {
                OrderDetails result = null;
                await connection.QueryAsync<OrderDetails, OrderDetails.OrderDetailsCustomer, OrderDetails.OrderDetailsLine, OrderDetails>
                    (GetOrderQuerySql, (details, customer, line) =>
                    {
                        if (result == null)
                            result = details;
                        result.OrderLines.Add(line);
                        result.Customer = customer;
                        return details;
                    }, new { orderId = request.OrderId },
                    splitOn: "customerId, productId");

                return result;
            });
        }
    }

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
            public int CustomerId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }
    }
}
