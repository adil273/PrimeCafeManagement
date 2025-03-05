using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeCafeManagement.Migrations
{
    /// <inheritdoc />
    public partial class init60 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MenuId",
                table: "Orders",
                column: "MenuId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_Menus_MenuId",
            //    table: "Orders",
            //    column: "MenuId",
            //    principalTable: "Menus",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_Menus_MenuId",
            //    table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MenuId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Orders");
        }
    }
}
