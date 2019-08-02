using System.Threading;
using System.Threading.Tasks;
using Checkout.Orders.Contracts.ApiModels;
using Checkout.Orders.Infrastructure;
using Dapper;
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
    o.[Status_Name] AS [Status],
    '' AS [Customer],
    o.[Customer_CustomerId] AS [CustomerId],
    o.[Customer_Email] AS [Email],
    o.[Customer_Phone] AS [Phone],
    o.[BillingAddress_Address] AS [Address],
    o.[BillingAddress_City] AS [City],
    o.[BillingAddress_FirstName] AS [FirstName],
    o.[BillingAddress_LastName] AS [LastName],
    o.[BillingAddress_Zip] AS [Zip],
    o.[ShippingAddress_Address] AS [Address],
    o.[ShippingAddress_City] AS [City],
    o.[ShippingAddress_FirstName] AS [FirstName],
    o.[ShippingAddress_LastName] AS [LastName],
    o.[ShippingAddress_Zip] AS [Zip],
    ol.[ProductId],
    ol.[ProductName],
    ol.[ProductPrice],
    ol.[Quantity],
    ol.[OrderId]
FROM .[dbo].[Order] o
LEFT JOIN [dbo].[OrderLines] ol ON o.Id = ol.OrderId
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
                await connection.QueryAsync<OrderDetails, OrderDetails.OrderDetailsCustomer, OrderDetails.OrderDetailsAddress, OrderDetails.OrderDetailsAddress, OrderDetails.OrderDetailsLine, OrderDetails>
                    (GetOrderQuerySql, (details, customer, billingAddress, shippingAddress, line) =>
                    {
                        if (result == null)
                            result = details;
                        result.OrderLines.Add(line);
                        result.Customer = customer;
                        result.BillingAddress = billingAddress;
                        result.ShippingAddress = shippingAddress;
                        return details;
                    }, new { orderId = request.OrderId },
                    splitOn: "Customer, Address, Address, ProductId");

                return result;
            });
        }
    }
}
