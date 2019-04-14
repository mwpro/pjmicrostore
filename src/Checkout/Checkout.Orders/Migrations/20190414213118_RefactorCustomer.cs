using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Checkout.Orders.Migrations
{
    public partial class RefactorCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Order_CustomerId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Order",
                newName: "Customer_CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "Customer_Email",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Phone",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Address",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_City",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_FirstName",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_LastName",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Zip",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Address",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_City",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_FirstName",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_LastName",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Zip",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer_Email",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Customer_Phone",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Address",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingAddress_City",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingAddress_FirstName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingAddress_LastName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Zip",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Address",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_City",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_FirstName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_LastName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Zip",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Customer_CustomerId",
                table: "Order",
                newName: "CustomerId");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
