using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class addWebConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblRoles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblWebConfigurations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfigKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfigValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWebConfigurations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblUserRoles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserRoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblUserRoles_tblRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tblRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserRoles_tblUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUsers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUserRoles_RoleId",
                table: "tblUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserRoles_UserID",
                table: "tblUserRoles",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUserRoles");

            migrationBuilder.DropTable(
                name: "tblWebConfigurations");

            migrationBuilder.DropTable(
                name: "tblRoles");

            migrationBuilder.DropTable(
                name: "tblUsers");
        }
    }
}
