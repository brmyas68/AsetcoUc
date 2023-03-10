using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UC.DataLayer.Migrations
{
    public partial class _init_UC_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UC_MenuLink",
                columns: table => new
                {
                    MenuLink_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuLink_TagID = table.Column<int>(type: "int", nullable: false),
                    MenuLink_FormTagID = table.Column<int>(type: "int", nullable: false),
                    MenuLink_ActionTagID = table.Column<int>(type: "int", nullable: false),
                    MenuLink_SystemTagID = table.Column<int>(type: "int", nullable: false),
                    MenuLink_TypeRouteID = table.Column<int>(type: "int", nullable: false),
                    MenuLink_ParentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_MenuLink", x => x.MenuLink_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UC_MenuLink");
        }
    }
}
