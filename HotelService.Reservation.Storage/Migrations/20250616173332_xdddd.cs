using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelService.Reservation.Storage.Migrations
{
    /// <inheritdoc />
    public partial class xdddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Promotions_PromotionId",
                schema: "Hotel",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Promotions",
                schema: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_PromotionId",
                schema: "Hotel",
                table: "Reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotions",
                schema: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PromotionId",
                schema: "Hotel",
                table: "Reservations",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Promotions_PromotionId",
                schema: "Hotel",
                table: "Reservations",
                column: "PromotionId",
                principalSchema: "Hotel",
                principalTable: "Promotions",
                principalColumn: "Id");
        }
    }
}
