using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Service.Rapor.Migrations
{
    /// <inheritdoc />
    public partial class RaporRaporBilgiEntityOlusturma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rapor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TalepEdildigiTarih = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Durum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rapor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RaporBilgi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RaporId = table.Column<Guid>(type: "uuid", nullable: false),
                    KonumBilgisi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    KisiSayisi = table.Column<int>(type: "integer", nullable: false),
                    TelefonNumarasiSayisi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaporBilgi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaporBilgi_Rapor_RaporId",
                        column: x => x.RaporId,
                        principalTable: "Rapor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rapor",
                columns: new[] { "Id", "Durum", "TalepEdildigiTarih" },
                values: new object[,]
                {
                    { new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1"), 2, new DateTimeOffset(new DateTime(2023, 6, 14, 13, 37, 1, 329, DateTimeKind.Unspecified).AddTicks(461), new TimeSpan(0, 3, 0, 0, 0)) },
                    { new Guid("e50ef18e-0947-11ee-b20b-54e1ad72c6a1"), 2, new DateTimeOffset(new DateTime(2023, 6, 14, 13, 37, 1, 329, DateTimeKind.Unspecified).AddTicks(502), new TimeSpan(0, 3, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "RaporBilgi",
                columns: new[] { "Id", "KisiSayisi", "KonumBilgisi", "RaporId", "TelefonNumarasiSayisi" },
                values: new object[,]
                {
                    { new Guid("07f4b594-0948-11ee-b20c-54e1ad72c6a1"), 2, "Ankara", new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1"), 3 },
                    { new Guid("7ea050d6-0948-11ee-b20d-54e1ad72c6a1"), 0, "Mersin", new Guid("e50ef18e-0947-11ee-b20b-54e1ad72c6a1"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RaporBilgi_RaporId",
                table: "RaporBilgi",
                column: "RaporId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaporBilgi");

            migrationBuilder.DropTable(
                name: "Rapor");
        }
    }
}
