using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UC.DataLayer.Migrations
{
    public partial class _init_UC_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_Description",
                table: "UC_User",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Description",
                table: "UC_User");

            migrationBuilder.DropColumn(
                name: "User_IsChecked",
                table: "UC_User");

            migrationBuilder.DropColumn(
                name: "User_IsFullData",
                table: "UC_User");
        }
    }
}
