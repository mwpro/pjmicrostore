using System.Linq;
using Checkout.Orders.Contracts.ApiModels;

namespace Checkout.Orders.Queries
{
    public static class OrderDetailsExtensions
    {
        public static decimal CalculateTotal(this OrderDetails orderDetails)
        {
            return orderDetails.OrderLines.Sum(x => x.Value) + orderDetails.Shipping.Fee + orderDetails.Payment.Fee;
        }
    }
}