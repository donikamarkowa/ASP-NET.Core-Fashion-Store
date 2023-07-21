using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStoreSystem.Data.Migrations
{
    public partial class AddIsActiveColumnForSoftDeletingProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("16b4adfe-bab2-4d76-9c81-4845d01a0ac9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("814886a1-1f79-476a-8829-d53e1ce6ccda"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("ed2cdda0-4a02-4168-a2ef-c24d37970eb8"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", "T-shirt boxy", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("fa963cb8-2851-435c-8b95-3e5cd9b76009"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ed2cdda0-4a02-4168-a2ef-c24d37970eb8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fa963cb8-2851-435c-8b95-3e5cd9b76009"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("16b4adfe-bab2-4d76-9c81-4845d01a0ac9"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("814886a1-1f79-476a-8829-d53e1ce6ccda"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", "Mini Dress", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });
        }
    }
}
