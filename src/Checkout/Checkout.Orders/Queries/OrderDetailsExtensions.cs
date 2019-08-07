using System.Linq;
using Checkout.Orders.Contracts.ApiModels;

namespace Checkout.Orders.Queries
{
    public static class OrderDetailsExtensions
    {
        public static decimal CalculateProductsTotal(this OrderDetails orderDetails)
        {
            return orderDetails.OrderLines.Sum(x => x.Value);
        }

        public static decimal CalculateTotal(this OrderDetails orderDetails)
        {
            return orderDetails.CalculateProductsTotal() + orderDetails.Shipping.Fee + orderDetails.Payment.Fee;
        }
    }
}