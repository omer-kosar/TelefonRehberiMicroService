using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Iletisim.Migrations
{
    /// <inheritdoc />
    public partial class IletisimEntityOlusturma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Iletisim",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KisiId = table.Column<Guid>(type: "uuid", nullable: false),
                    IletisimType = table.Column<int>(type: "integer", nullable: false),
                    Icerik = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iletisim", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Iletisim");
        }
    }
}
