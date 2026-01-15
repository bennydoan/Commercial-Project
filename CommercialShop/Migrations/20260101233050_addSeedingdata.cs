using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CommercialShop.Migrations
{
    /// <inheritdoc />
    public partial class addSeedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "gender" },
                values: new object[,]
                {
                    { 1, "Jackets", "Female" },
                    { 2, "Jerseys", "Female" },
                    { 3, "Scarves", "Female" },
                    { 4, "Tops", "Female" },
                    { 5, "Training", "Female" },
                    { 6, "Footwear", "Female" },
                    { 7, "Jackets", "Male" },
                    { 8, "Jerseys", "Male" },
                    { 9, "Tops", "Male" },
                    { 10, "Training", "Male" },
                    { 11, "Footwear", "Male" },
                    { 12, "Trousers & Shorts", "Male" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "SizeId", "CodeSize" },
                values: new object[,]
                {
                    { 1, "S" },
                    { 2, "M" },
                    { 3, "L" },
                    { 4, "XL" },
                    { 5, "2XL" },
                    { 6, "3XL" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Price", "ProductDescription", "ProductName" },
                values: new object[,]
                {
                    { new Guid("2b6f0a30-1f7a-4d7f-b9a7-111111111111"), 4, 172.00m, "Official 2025-26 Away Stadium Shirt.", "Chelsea Nike Away Stadium Shirt 2025-26" },
                    { new Guid("3c7f1b41-2f8b-5e8f-c8b8-222222222222"), 9, 172.00m, "Official 2025-26 Away Stadium Shirt.", "Chelsea Nike Stadium Shirt 2025-26" }
                });

            migrationBuilder.InsertData(
                table: "ItemVariants",
                columns: new[] { "VariantId", "ProductId", "SizeId", "StockQuantity" },
                values: new object[,]
                {
                    { new Guid("4d8f2c52-3f9c-6f90-d9c9-333333333333"), new Guid("2b6f0a30-1f7a-4d7f-b9a7-111111111111"), 1, 21 },
                    { new Guid("5e9f3d63-4fad-7fa1-eada-444444444444"), new Guid("2b6f0a30-1f7a-4d7f-b9a7-111111111111"), 2, 22 },
                    { new Guid("6faf4e74-5bbe-8fb2-fbeb-555555555555"), new Guid("2b6f0a30-1f7a-4d7f-b9a7-111111111111"), 3, 29 },
                    { new Guid("70a05f85-6ccf-9fc3-0cbc-666666666666"), new Guid("3c7f1b41-2f8b-5e8f-c8b8-222222222222"), 1, 23 },
                    { new Guid("81b16a96-7dd0-afd4-1cdc-777777777777"), new Guid("3c7f1b41-2f8b-5e8f-c8b8-222222222222"), 2, 25 },
                    { new Guid("92c27ba7-8ee1-bfe5-2ded-888888888888"), new Guid("3c7f1b41-2f8b-5e8f-c8b8-222222222222"), 3, 20 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageId", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { 1, "~/Image/Women/Top/womenHomeShirt.png", new Guid("2b6f0a30-1f7a-4d7f-b9a7-111111111111") },
                    { 2, "~/Image/Men/Top/ChelseaAwayTshirt.png", new Guid("3c7f1b41-2f8b-5e8f-c8b8-222222222222") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ItemVariants",
                keyColumn: "VariantId",
                keyValue: new Guid("4d8f2c52-3f9c-6f90-d9c9-333333333333"));

            migrationBuilder.DeleteData(
                table: "ItemVariants",
                keyColumn: "VariantId",
                keyValue: new Guid("5e9f3d63-4fad-7fa1-eada-444444444444"));

            migrationBuilder.DeleteData(
                table: "ItemVariants",
                keyColumn: "VariantId",
                keyValue: new Guid("6faf4e74-5bbe-8fb2-fbeb-555555555555"));

            migrationBuilder.DeleteData(
                table: "ItemVariants",
                keyColumn: "VariantId",
                keyValue: new Guid("70a05f85-6ccf-9fc3-0cbc-666666666666"));

            migrationBuilder.DeleteData(
                table: "ItemVariants",
                keyColumn: "VariantId",
                keyValue: new Guid("81b16a96-7dd0-afd4-1cdc-777777777777"));

            migrationBuilder.DeleteData(
                table: "ItemVariants",
                keyColumn: "VariantId",
                keyValue: new Guid("92c27ba7-8ee1-bfe5-2ded-888888888888"));

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "SizeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "SizeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "SizeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("2b6f0a30-1f7a-4d7f-b9a7-111111111111"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3c7f1b41-2f8b-5e8f-c8b8-222222222222"));

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "SizeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "SizeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "SizeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);
        }
    }
}
