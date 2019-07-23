using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Infrastructure;
using Dapper;
using MediatR;

namespace Checkout.Orders.Queries
{
    public class GetUserOrderListQuery : IRequest<IEnumerable<OrderListItem>>
    {
        public Guid CustomerId { get; }

        public GetUserOrderListQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }

    public class GetUserOrderListQueryHandler : IRequestHandler<GetUserOrderListQuery, IEnumerable<OrderListItem>>
    {
        private readonly IDatabase _database;

        private const string GetOrderListSql =
            @"SELECT 
    o.Id, 
    o.CreateDate AS CreateDateUtc,
    Customer_Email AS Customer,
    SUM(ol.ProductPrice * ol.Quantity) AS Amount,
    o.Status_Name AS Status
FROM [Order] o
LEFT JOIN OrderLines ol ON o.Id = ol.OrderId
WHERE o.Customer_CustomerId = @customerId
GROUP BY o.Id, o.CreateDate, o.Customer_Email, o.Status_Name
ORDER BY o.CreateDate DESC";

        public GetUserOrderListQueryHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<OrderListItem>> Handle(GetUserOrderListQuery request, CancellationToken cancellationToken)
        {
            return await _database.ExecuteOnConnection(async connection =>
            {
                return await connection.QueryAsync<OrderListItem>(GetOrderListSql, new
                {
                    customerId = request.CustomerId
                });
            });
        }
    }
}