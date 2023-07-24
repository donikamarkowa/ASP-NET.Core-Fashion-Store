using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStoreSystem.Data.Migrations
{
    public partial class AddNewColumnWalletForUserMoney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProducts");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7f20ab6b-04f7-4b96-83e3-ded289a27808"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("df7c3385-678f-4c59-bc99-f0735c34211c"));

            migrationBuilder.AddColumn<decimal>(
                name: "Wallet",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("549172f5-e545-466d-a077-76ec9be9bf22"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", "T-shirt boxy", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("af64ef5e-049f-4542-b515-0bd67bc5e9fc"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("549172f5-e545-466d-a077-76ec9be9bf22"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("af64ef5e-049f-4542-b515-0bd67bc5e9fc"));

            migrationBuilder.DropColumn(
                name: "Wallet",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CartProducts",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProducts", x => new { x.UserId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CartProducts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsActive", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("7f20ab6b-04f7-4b96-83e3-ded289a27808"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", false, "T-shirt boxy", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsActive", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("df7c3385-678f-4c59-bc99-f0735c34211c"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", false, "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProducts",
                column: "ProductId");
        }
    }
}
