using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class updateUserRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUserRoles_tblUsers_UserID",
                table: "tblUserRoles");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "tblUserRoles");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "tblUserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tblUserRoles_UserID",
                table: "tblUserRoles",
                newName: "IX_tblUserRoles_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "tblUserRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserRoles_tblUsers_UserId",
                table: "tblUserRoles",
                column: "UserId",
                principalTable: "tblUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUserRoles_tblUsers_UserId",
                table: "tblUserRoles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tblUserRoles",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_tblUserRoles_UserId",
                table: "tblUserRoles",
                newName: "IX_tblUserRoles_UserID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "tblUserRoles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "tblUserRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserRoles_tblUsers_UserID",
                table: "tblUserRoles",
                column: "UserID",
                principalTable: "tblUsers",
                principalColumn: "ID");
        }
    }
}
