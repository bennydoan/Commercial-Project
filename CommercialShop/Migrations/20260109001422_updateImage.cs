using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CommercialShop.Migrations
{
    /// <inheritdoc />
    public partial class updateImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShownFirst",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 1,
                columns: new[] { "ImagePath", "IsShownFirst" },
                values: new object[] { "/Image/Women/Top/womenHomeShirt.png", true });

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 2,
                columns: new[] { "ImagePath", "IsShownFirst" },
                values: new object[] { "/Image/Men/Top/ChelseaAwayTshirt.png", true });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageId", "ImagePath", "IsShownFirst", "ProductId" },
                values: new object[,]
                {
                    { 3, "/Image/HomePage/game.png", false, new Guid("2b6f0a30-1f7a-4d7f-b9a7-111111111111") },
                    { 4, "/Image/HomePage/kit1.png", true, new Guid("3c7f1b41-2f8b-5e8f-c8b8-222222222222") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "IsShownFirst",
                table: "ProductImages");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 1,
                column: "ImagePath",
                value: "~/Image/Women/Top/womenHomeShirt.png");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "ImageId",
                keyValue: 2,
                column: "ImagePath",
                value: "~/Image/Men/Top/ChelseaAwayTshirt.png");
        }
    }
}
