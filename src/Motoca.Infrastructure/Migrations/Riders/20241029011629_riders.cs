using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motoca.Infrastructure.Migrations.Riders
{
    /// <inheritdoc />
    public partial class riders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "riders",
                columns: table => new
                {
                    InternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SocialId = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_riders", x => x.InternalId);
                });

            migrationBuilder.CreateTable(
                name: "riders_drivers_licenses",
                columns: table => new
                {
                    InternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Category = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    RiderEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_riders_drivers_licenses", x => x.InternalId);
                    table.ForeignKey(
                        name: "FK_riders_drivers_licenses_riders_RiderEntityId",
                        column: x => x.RiderEntityId,
                        principalTable: "riders",
                        principalColumn: "InternalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_riders_Id",
                table: "riders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_riders_SocialId",
                table: "riders",
                column: "SocialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_riders_drivers_licenses_RiderEntityId",
                table: "riders_drivers_licenses",
                column: "RiderEntityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "riders_drivers_licenses");

            migrationBuilder.DropTable(
                name: "riders");
        }
    }
}
