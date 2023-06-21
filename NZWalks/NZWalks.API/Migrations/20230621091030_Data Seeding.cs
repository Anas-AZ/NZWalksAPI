using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("17dc1406-2245-4270-8611-2587e9c44e74"), "Easy" },
                    { new Guid("d5af210e-03b1-4066-b9b0-84e14967c3ea"), "Hard" },
                    { new Guid("eed37763-851d-452d-b429-e4964a78d5b7"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("166b7240-6e03-49af-b2c5-98de777adb41"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("461f524a-609f-426b-b9f7-15d0e96e245a"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("65d76290-9091-4646-a786-72bf53255779"), "BOP", "Bay Of Plenty", null },
                    { new Guid("b0026d9e-b538-49c5-a0ae-3102974d53ca"), "STL", "Southland", null },
                    { new Guid("d7b9dc43-bc39-41ea-94c8-9b8944d2d362"), "NTL", "Northland", null },
                    { new Guid("d96e07cf-ff1f-4dfb-892c-86a8b8e8c287"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("17dc1406-2245-4270-8611-2587e9c44e74"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d5af210e-03b1-4066-b9b0-84e14967c3ea"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("eed37763-851d-452d-b429-e4964a78d5b7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("166b7240-6e03-49af-b2c5-98de777adb41"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("461f524a-609f-426b-b9f7-15d0e96e245a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("65d76290-9091-4646-a786-72bf53255779"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b0026d9e-b538-49c5-a0ae-3102974d53ca"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d7b9dc43-bc39-41ea-94c8-9b8944d2d362"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d96e07cf-ff1f-4dfb-892c-86a8b8e8c287"));
        }
    }
}
