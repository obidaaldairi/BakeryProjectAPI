using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class updateentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAT",
                table: "tblUsers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "tblUsers",
                newName: "EnglishUserName");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "tblUsers",
                newName: "EnglishRole");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "tblUsers",
                newName: "EnglishBio");

            migrationBuilder.AddColumn<string>(
                name: "ArabicBio",
                table: "tblUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArabicRole",
                table: "tblUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArabicUserName",
                table: "tblUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArabicRoleName",
                table: "tblRoles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabicBio",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "ArabicRole",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "ArabicUserName",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "ArabicRoleName",
                table: "tblRoles");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tblUsers",
                newName: "CreatedAT");

            migrationBuilder.RenameColumn(
                name: "EnglishUserName",
                table: "tblUsers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "EnglishRole",
                table: "tblUsers",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "EnglishBio",
                table: "tblUsers",
                newName: "Bio");
        }
    }
}
