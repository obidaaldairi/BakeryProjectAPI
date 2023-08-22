using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddUserVerificationTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblUserVerification",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVerify = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserVerification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblUserVerification_tblUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUserVerification_UserID",
                table: "tblUserVerification",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUserVerification");
        }
    }
}
