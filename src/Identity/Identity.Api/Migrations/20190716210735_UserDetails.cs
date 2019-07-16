using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Api.Migrations
{
    public partial class UserDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Street",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Zip",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Street",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Zip",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress_City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Street",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Zip",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Street",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Zip",
                table: "AspNetUsers");
        }
    }
}
