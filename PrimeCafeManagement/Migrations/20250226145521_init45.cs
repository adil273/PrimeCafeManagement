using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeCafeManagement.Migrations
{
    /// <inheritdoc />
    public partial class init45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "OrderSummaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrderSummaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderSummaries_UserId",
                table: "OrderSummaries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSummaries_Users_UserId",
                table: "OrderSummaries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderSummaries_Users_UserId",
                table: "OrderSummaries");

            migrationBuilder.DropIndex(
                name: "IX_OrderSummaries_UserId",
                table: "OrderSummaries");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "OrderSummaries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderSummaries");
        }
    }
}
