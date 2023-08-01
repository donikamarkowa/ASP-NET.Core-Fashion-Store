using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStoreSystem.Data.Migrations
{
    public partial class AddUserFirstNameAndLastNameAndNewCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("549172f5-e545-466d-a077-76ec9be9bf22"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("af64ef5e-049f-4542-b515-0bd67bc5e9fc"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Test");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Testov");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 8, "Swimsuit" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("4748509c-842a-496a-b414-cc6dec72b7c1"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", "T-shirt boxy", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("e35e4efd-0e62-431d-acd5-649be41f8a9f"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4748509c-842a-496a-b414-cc6dec72b7c1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e35e4efd-0e62-431d-acd5-649be41f8a9f"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsActive", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("549172f5-e545-466d-a077-76ec9be9bf22"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", false, "T-shirt boxy", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsActive", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("af64ef5e-049f-4542-b515-0bd67bc5e9fc"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", false, "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });
        }
    }
}
