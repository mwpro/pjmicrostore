using Microsoft.EntityFrameworkCore.Migrations;

namespace Checkout.Orders.Migrations
{
    public partial class RefactorOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Order",
                newName: "Status_Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status_Name",
                table: "Order",
                newName: "Status");
        }
    }
}
