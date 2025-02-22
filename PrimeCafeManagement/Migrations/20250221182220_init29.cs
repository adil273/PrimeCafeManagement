using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeCafeManagement.Migrations
{
    /// <inheritdoc />
    public partial class init29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuTitleId",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MenuPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuTitles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuTitleId",
                table: "Menus",
                column: "MenuTitleId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Menus_MenuTitles_MenuTitleId",
            //    table: "Menus",
            //    column: "MenuTitleId",
            //    principalTable: "MenuTitles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Menus_MenuTitles_MenuTitleId",
            //    table: "Menus");

            migrationBuilder.DropTable(
                name: "MenuPrices");

            migrationBuilder.DropTable(
                name: "MenuTitles");

            migrationBuilder.DropIndex(
                name: "IX_Menus_MenuTitleId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "MenuTitleId",
                table: "Menus");
        }
    }
}
