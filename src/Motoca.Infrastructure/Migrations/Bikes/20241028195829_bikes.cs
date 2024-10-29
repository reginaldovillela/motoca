using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motoca.Infrastructure.Migrations.Bikes
{
    /// <inheritdoc />
    public partial class bikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bikes",
                columns: table => new
                {
                    InternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LicensePlate = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bikes", x => x.InternalId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bikes_Id",
                table: "bikes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bikes_LicensePlate",
                table: "bikes",
                column: "LicensePlate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bikes");
        }
    }
}
