using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.Kisi.Migrations
{
    /// <inheritdoc />
    public partial class KisiEntitySeedDataEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kisi",
                columns: new[] { "Id", "Ad", "Firma", "Soyad" },
                values: new object[,]
                {
                    { new Guid("856b39fb-d802-4574-a0b4-872a12589c59"), "John", "ABC Corporation", "Doe" },
                    { new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"), "Jane", " XYZ Ltd.", "Smith" },
                    { new Guid("a5ce0ec5-1c1a-4a15-86f9-cdde439a92a2"), "Michael", "QWERTY Holdings", "Johnson" },
                    { new Guid("e0207c71-f788-4067-8ebf-3a48e7b6966e"), "Emily", "XYZ Ltd.", "Brown" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kisi",
                keyColumn: "Id",
                keyValue: new Guid("856b39fb-d802-4574-a0b4-872a12589c59"));

            migrationBuilder.DeleteData(
                table: "Kisi",
                keyColumn: "Id",
                keyValue: new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"));

            migrationBuilder.DeleteData(
                table: "Kisi",
                keyColumn: "Id",
                keyValue: new Guid("a5ce0ec5-1c1a-4a15-86f9-cdde439a92a2"));

            migrationBuilder.DeleteData(
                table: "Kisi",
                keyColumn: "Id",
                keyValue: new Guid("e0207c71-f788-4067-8ebf-3a48e7b6966e"));
        }
    }
}
