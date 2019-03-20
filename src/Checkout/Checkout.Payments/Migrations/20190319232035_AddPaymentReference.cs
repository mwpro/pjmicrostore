using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Checkout.Payments.Migrations
{
    public partial class AddPaymentReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentReference",
                table: "Payments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentReference",
                table: "Payments");
        }
    }
}
