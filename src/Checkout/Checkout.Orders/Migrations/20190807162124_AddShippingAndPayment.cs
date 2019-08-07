using Microsoft.EntityFrameworkCore.Migrations;

namespace Checkout.Orders.Migrations
{
    public partial class AddShippingAndPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Delivery_Fee",
                table: "Order",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Delivery_Name",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Payment_Fee",
                table: "Order",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Payment_Name",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivery_Fee",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Delivery_Name",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Payment_Fee",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Payment_Name",
                table: "Order");
        }
    }
}
