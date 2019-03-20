using Microsoft.EntityFrameworkCore.Migrations;

namespace Checkout.Payments.Migrations
{
    public partial class AddPaymentDescriptionToMock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentDescription",
                table: "MockPayments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDescription",
                table: "MockPayments");
        }
    }
}
