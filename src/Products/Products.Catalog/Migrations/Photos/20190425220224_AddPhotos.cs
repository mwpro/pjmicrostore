using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Products.Catalog.Migrations.Photos
{
    public partial class AddPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    PhotoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OriginalUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => new { x.ProductId, x.PhotoId });
                });

            migrationBuilder.CreateTable(
                name: "PhotoVariant",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    PhotoId = table.Column<int>(nullable: false),
                    PhotoVariantId = table.Column<int>(nullable: false),
                    VariantName = table.Column<string>(nullable: true),
                    VariantUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoVariant", x => new { x.ProductId, x.PhotoId, x.PhotoVariantId });
                    table.ForeignKey(
                        name: "FK_PhotoVariant_Photos_ProductId_PhotoId",
                        columns: x => new { x.ProductId, x.PhotoId },
                        principalTable: "Photos",
                        principalColumns: new[] { "ProductId", "PhotoId" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoVariant");

            migrationBuilder.DropTable(
                name: "Photos");
        }
    }
}
