using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeCafeManagement.Migrations
{
    /// <inheritdoc />
    public partial class init47 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderSummaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderSummaries_OrderId",
                table: "OrderSummaries",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSummaries_Orders_OrderId",
                table: "OrderSummaries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderSummaries_Orders_OrderId",
                table: "OrderSummaries");

            migrationBuilder.DropIndex(
                name: "IX_OrderSummaries_OrderId",
                table: "OrderSummaries");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderSummaries");
        }
    }
}
