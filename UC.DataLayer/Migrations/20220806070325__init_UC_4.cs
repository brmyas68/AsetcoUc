using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UC.DataLayer.Migrations
{
    public partial class _init_UC_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuLink_Icon",
                table: "UC_MenuLink",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuLink_NavigationPath",
                table: "UC_MenuLink",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuLink_Icon",
                table: "UC_MenuLink");

            migrationBuilder.DropColumn(
                name: "MenuLink_NavigationPath",
                table: "UC_MenuLink");
        }
    }
}
