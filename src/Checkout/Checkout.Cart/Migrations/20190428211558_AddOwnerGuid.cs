using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Checkout.Cart.Migrations
{
    public partial class AddOwnerGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerUserId",
                table: "Carts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Carts");
        }
    }
}
