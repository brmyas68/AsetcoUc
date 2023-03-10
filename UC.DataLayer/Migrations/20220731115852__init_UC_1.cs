using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UC.DataLayer.Migrations
{
    public partial class _init_UC_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UC_City",
                columns: table => new
                {
                    City_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City_Province_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_City", x => x.City_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_CompanyInfo",
                columns: table => new
                {
                    CompanyInfo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyInfo_TagID = table.Column<int>(type: "int", nullable: false),
                    CompanyInfo_Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CompanyInfo_Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CompanyInfo_Mobile = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CompanyInfo_Fax = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CompanyInfo_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyInfo_Site = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CompanyInfo_Instagram = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyInfo_About = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    CompanyInfo_SmsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyInfo_LanguageID = table.Column<int>(type: "int", nullable: false),
                    CompanyInfo_TypeDateTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_CompanyInfo", x => x.CompanyInfo_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_Language",
                columns: table => new
                {
                    Language_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Language_Rtl = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    Language_Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Language_PreNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_Language", x => x.Language_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_Log",
                columns: table => new
                {
                    Log_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Log_UserID = table.Column<int>(type: "int", nullable: false),
                    Log_RouteStructureID = table.Column<int>(type: "int", nullable: false),
                    Log_DateTime = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_Log", x => x.Log_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_Province",
                columns: table => new
                {
                    Province_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Province_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_Province", x => x.Province_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_Role",
                columns: table => new
                {
                    Role_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_TagID = table.Column<int>(type: "int", nullable: false),
                    Role_TenantID = table.Column<int>(type: "int", nullable: false),
                    Role_IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_Role", x => x.Role_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_RoleMembers",
                columns: table => new
                {
                    RoleMembers_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleMembers_RoleID = table.Column<int>(type: "int", nullable: false),
                    RoleMembers_RoleMemberID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_RoleMembers", x => x.RoleMembers_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_RoleUserPermission",
                columns: table => new
                {
                    RoleUserPermission_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleUserPermission_RoleID_UserID = table.Column<int>(type: "int", nullable: false),
                    RoleUserPermission_RouteStructureID = table.Column<int>(type: "int", nullable: false),
                    RoleUserPermission_ParentID = table.Column<int>(type: "int", nullable: false),
                    RoleUserPermission_BitMerge = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_RoleUserPermission", x => x.RoleUserPermission_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_RouteStructure",
                columns: table => new
                {
                    RouteStructure_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteStructure_TenantID = table.Column<int>(type: "int", nullable: false),
                    RouteStructure_ParentID = table.Column<int>(type: "int", nullable: false),
                    RouteStructure_Tagsknowledge_ID = table.Column<int>(type: "int", nullable: false),
                    RouteStructure_TypeRoute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_RouteStructure", x => x.RouteStructure_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_Sms",
                columns: table => new
                {
                    Sms_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sms_Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sms_ActiveCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Sms_Time = table.Column<DateTime>(type: "datetime2", maxLength: 5, nullable: false),
                    Sms_IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Sms_Status = table.Column<int>(type: "int", nullable: false),
                    Sms_MessageID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_Sms", x => x.Sms_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_Tagsknowledge",
                columns: table => new
                {
                    Tagsknowledge_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tagsknowledge_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tagsknowledge_Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_Tagsknowledge", x => x.Tagsknowledge_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_Token",
                columns: table => new
                {
                    Token_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token_UserID = table.Column<int>(type: "int", nullable: false),
                    Token_HashCode = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Token_DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token_DateExpire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token_DateLastAccessTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Token_IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_Token", x => x.Token_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_TranslateTags",
                columns: table => new
                {
                    TranslateTags_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranslateTags_Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TranslateTags_TagsknowledgeID = table.Column<int>(type: "int", nullable: false),
                    TranslateTags_LanguageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_TranslateTags", x => x.TranslateTags_ID);
                });

            migrationBuilder.CreateTable(
                name: "UC_User",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_TenantID = table.Column<int>(type: "int", nullable: false),
                    User_FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_IdentifyNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    User_Gender = table.Column<int>(type: "int", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    User_Tell = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    User_DateRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_HashPassword = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    User_Province_ID = table.Column<int>(type: "int", nullable: false),
                    User_City_ID = table.Column<int>(type: "int", nullable: false),
                    User_Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    User_IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    User_IsBlock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_User", x => x.User_ID);
                });

            migrationBuilder.Sql("DBCC CHECKIDENT ('User_ID', RESEED, 1000)");


            migrationBuilder.CreateTable(
                name: "UC_UserRole",
                columns: table => new
                {
                    UserRole_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Role_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC_UserRole", x => x.UserRole_ID);
                });

            migrationBuilder.CreateIndex(
                name: "Index_LogRouteStructureID",
                table: "UC_Log",
                column: "Log_RouteStructureID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_LogUserID",
                table: "UC_Log",
                column: "Log_UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_ActiveCode",
                table: "UC_Sms",
                column: "Sms_ActiveCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_HashCode",
                table: "UC_Token",
                column: "Token_HashCode");

            migrationBuilder.CreateIndex(
                name: "Index_UserName",
                table: "UC_User",
                column: "User_UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UC_City");

            migrationBuilder.DropTable(
                name: "UC_CompanyInfo");

            migrationBuilder.DropTable(
                name: "UC_Language");

            migrationBuilder.DropTable(
                name: "UC_Log");

            migrationBuilder.DropTable(
                name: "UC_Province");

            migrationBuilder.DropTable(
                name: "UC_Role");

            migrationBuilder.DropTable(
                name: "UC_RoleMembers");

            migrationBuilder.DropTable(
                name: "UC_RoleUserPermission");

            migrationBuilder.DropTable(
                name: "UC_RouteStructure");

            migrationBuilder.DropTable(
                name: "UC_Sms");

            migrationBuilder.DropTable(
                name: "UC_Tagsknowledge");

            migrationBuilder.DropTable(
                name: "UC_Token");

            migrationBuilder.DropTable(
                name: "UC_TranslateTags");

            migrationBuilder.DropTable(
                name: "UC_User");

            migrationBuilder.DropTable(
                name: "UC_UserRole");
        }
    }
}
