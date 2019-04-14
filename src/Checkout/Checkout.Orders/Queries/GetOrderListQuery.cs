﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Infrastructure;
using Dapper;
using MediatR;

namespace Checkout.Orders.Queries
{
    public class GetOrderListQuery : IRequest<IEnumerable<OrderListItem>>
    {

    }

    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IEnumerable<OrderListItem>>
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
GROUP BY o.Id, o.CreateDate, o.Customer_Email, o.Status_Name
ORDER BY o.CreateDate DESC";

        public GetOrderListQueryHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<OrderListItem>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            return await _database.ExecuteOnConnection(async connection =>
            {
                return await connection.QueryAsync<OrderListItem>(GetOrderListSql);
            });
        }
    }

    public class OrderListItem
    {
        public int Id { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public string Customer { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
