using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.Iletisim.Migrations
{
    /// <inheritdoc />
    public partial class IletisimEntitySeedDataEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Iletisim",
                columns: new[] { "Id", "Icerik", "IletisimType", "KisiId" },
                values: new object[,]
                {
                    { new Guid("09a87d0c-7cc6-4ddd-affd-34699cc1547f"), "05383946677", 1, new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0") },
                    { new Guid("7cfac4e8-a901-4627-9c24-f409c86a30b1"), "05383948899", 1, new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0") },
                    { new Guid("9de5438f-4237-4ec5-b081-d54ed8cb64e7"), "Ankara", 3, new Guid("856b39fb-d802-4574-a0b4-872a12589c59") },
                    { new Guid("bd999251-da45-418d-8604-1f69d552fc3f"), "abc@abc.com", 2, new Guid("856b39fb-d802-4574-a0b4-872a12589c59") },
                    { new Guid("d27306b2-1c2e-47f9-96ef-ec91b02e83bf"), "05383941232", 1, new Guid("856b39fb-d802-4574-a0b4-872a12589c59") },
                    { new Guid("e51d2709-dcc1-4016-8c0c-9936177c1e67"), "Ankara", 3, new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Iletisim",
                keyColumn: "Id",
                keyValue: new Guid("09a87d0c-7cc6-4ddd-affd-34699cc1547f"));

            migrationBuilder.DeleteData(
                table: "Iletisim",
                keyColumn: "Id",
                keyValue: new Guid("7cfac4e8-a901-4627-9c24-f409c86a30b1"));

            migrationBuilder.DeleteData(
                table: "Iletisim",
                keyColumn: "Id",
                keyValue: new Guid("9de5438f-4237-4ec5-b081-d54ed8cb64e7"));

            migrationBuilder.DeleteData(
                table: "Iletisim",
                keyColumn: "Id",
                keyValue: new Guid("bd999251-da45-418d-8604-1f69d552fc3f"));

            migrationBuilder.DeleteData(
                table: "Iletisim",
                keyColumn: "Id",
                keyValue: new Guid("d27306b2-1c2e-47f9-96ef-ec91b02e83bf"));

            migrationBuilder.DeleteData(
                table: "Iletisim",
                keyColumn: "Id",
                keyValue: new Guid("e51d2709-dcc1-4016-8c0c-9936177c1e67"));
        }
    }
}
