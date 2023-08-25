using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class DeleteRoleTextsFromTableUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabicRole",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "EnglishRole",
                table: "tblUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArabicRole",
                table: "tblUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishRole",
                table: "tblUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
