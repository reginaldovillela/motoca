using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motoca.Infrastructure.Migrations.Rentals
{
    /// <inheritdoc />
    public partial class rentals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rentals_plans",
                columns: table => new
                {
                    InternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DurationTime = table.Column<short>(type: "smallint", nullable: false),
                    ValuePerDay = table.Column<decimal>(type: "decimal", nullable: false),
                    PenaltyPercent = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentals_plans", x => x.InternalId);
                });

            migrationBuilder.CreateTable(
                name: "rentals",
                columns: table => new
                {
                    InternalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RiderId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BikeId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AmountToPay = table.Column<decimal>(type: "decimal", nullable: false),
                    PlanEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentals", x => x.InternalId);
                    table.ForeignKey(
                        name: "FK_rentals_rentals_plans_PlanEntityId",
                        column: x => x.PlanEntityId,
                        principalTable: "rentals_plans",
                        principalColumn: "InternalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rentals_Id",
                table: "rentals",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rentals_PlanEntityId",
                table: "rentals",
                column: "PlanEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_rentals_plans_Id",
                table: "rentals_plans",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rentals");

            migrationBuilder.DropTable(
                name: "rentals_plans");
        }
    }
}
