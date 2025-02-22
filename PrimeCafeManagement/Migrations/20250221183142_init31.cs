using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeCafeManagement.Migrations
{
    /// <inheritdoc />
    public partial class init31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuPriceId",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuPriceId",
                table: "Menus",
                column: "MenuPriceId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Menus_MenuPrices_MenuPriceId",
            //    table: "Menus",
            //    column: "MenuPriceId",
            //    principalTable: "MenuPrices",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Menus_MenuPrices_MenuPriceId",
            //    table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_MenuPriceId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "MenuPriceId",
                table: "Menus");
        }
    }
}
