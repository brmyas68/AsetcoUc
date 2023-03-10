using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UC.DataLayer.Migrations
{
    public partial class _init_UC_10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_IsChecked",
                table: "UC_User");

            migrationBuilder.DropColumn(
                name: "User_IsFullData",
                table: "UC_User");

            migrationBuilder.AddColumn<bool>(
                name: "Role_ReadOnly",
                table: "UC_Role",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role_ReadOnly",
                table: "UC_Role");

            migrationBuilder.AddColumn<bool>(
                name: "User_IsChecked",
                table: "UC_User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "User_IsFullData",
                table: "UC_User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
