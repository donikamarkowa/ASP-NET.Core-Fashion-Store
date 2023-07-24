﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStoreSystem.Data.Migrations
{
    public partial class UpdateNameToCartProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("7f20ab6b-04f7-4b96-83e3-ded289a27808"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", "T-shirt boxy", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("df7c3385-678f-4c59-bc99-f0735c34211c"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7f20ab6b-04f7-4b96-83e3-ded289a27808"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("df7c3385-678f-4c59-bc99-f0735c34211c"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsActive", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("3b44fe42-ec41-4c11-8715-4ed8fffe0afb"), 1, "The dress has narrow straps slits on the sides with a composition of 65% viscose, 32% polyamide, 3% elastane.", "https://static.reserved.com/media/catalog/product/2/6/2690T-99X-001-1-687951_3.jpg", false, "Mini Dress", 40m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "XS" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsActive", "Name", "Price", "SellerId", "Size" },
                values: new object[] { new Guid("75d70afe-f066-4a28-9898-f2848a754aa8"), 2, "Kimono-style t-shirt in high-cotton jersey with a round neckline and short sleeves.", "https://static.reserved.com/media/catalog/product/2/2/2272T-99X-001-1-633007_2.jpg", false, "T-shirt boxy", 13m, new Guid("1bb98123-e199-4f38-8123-4a2e6b0f7814"), "S" });
        }
    }
}
