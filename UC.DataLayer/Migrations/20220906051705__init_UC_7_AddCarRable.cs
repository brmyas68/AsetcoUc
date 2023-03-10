using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UC.DataLayer.Migrations
{
    public partial class _init_UC_7_AddCarRable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UC_BrandCar",
                columns: table => new
                {
                    BrandCar_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandCar_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_BrandCar", x => x.BrandCar_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_ModelCar",
                columns: table => new
                {
                    ModelCar_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelCar_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ModelCar_BrandCarID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_ModelCar", x => x.ModelCar_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UC_BrandCar");

            migrationBuilder.DropTable(
                name: "UC_ModelCar");
        }
    }
}
