using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStoreSystem.Data.Migrations
{
    public partial class AddNewEntityCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8610d1b9-5196-4198-8c99-6b9206d4463a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c15a4d57-0dcc-4c13-94a1-cf5f17dd754e"));

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
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("3b44fe42-ec41-4c11-8715-4ed8fffe0afb"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("75d70afe-f066-4a28-9898-f2848a754aa8"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", "T-shirt boxy", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProducts");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3b44fe42-ec41-4c11-8715-4ed8fffe0afb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("75d70afe-f066-4a28-9898-f2848a754aa8"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsActive", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("8610d1b9-5196-4198-8c99-6b9206d4463a"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", false, "T-shirt boxy", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsActive", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("c15a4d57-0dcc-4c13-94a1-cf5f17dd754e"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", false, "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });
        }
    }
}
