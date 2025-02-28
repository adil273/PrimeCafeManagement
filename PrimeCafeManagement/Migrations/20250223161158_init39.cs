using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeCafeManagement.Migrations
{
    /// <inheritdoc />
    public partial class init39 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrderStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_UserId",
                table: "OrderStatuses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatuses_Users_UserId",
                table: "OrderStatuses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatuses_Users_UserId",
                table: "OrderStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatuses_UserId",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderStatuses");
        }
    }
}
