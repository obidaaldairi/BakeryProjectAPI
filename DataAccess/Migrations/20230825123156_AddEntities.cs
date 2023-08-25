using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAdmins",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAdmins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblAdmins_tblUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblCategories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArabicTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnglishTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tblProviders",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProviders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblProviders_tblUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblUserInformations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserInformations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblUserInformations_tblUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProducts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnglishProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArabicDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblProducts_tblCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "tblCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblCarts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCarts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblCarts_tblProducts_ProductID",
                        column: x => x.ProductID,
                        principalTable: "tblProducts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCarts_tblUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductImages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblProductImages_tblProducts_ProductID",
                        column: x => x.ProductID,
                        principalTable: "tblProducts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductProviders",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductProviders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblProductProviders_tblProducts_ProductID",
                        column: x => x.ProductID,
                        principalTable: "tblProducts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblProductProviders_tblProviders_ProviderID",
                        column: x => x.ProviderID,
                        principalTable: "tblProviders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAdmins_UserID",
                table: "tblAdmins",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCarts_ProductID",
                table: "tblCarts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCarts_UserID",
                table: "tblCarts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductImages_ProductID",
                table: "tblProductImages",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductProviders_ProductID",
                table: "tblProductProviders",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductProviders_ProviderID",
                table: "tblProductProviders",
                column: "ProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_CategoryID",
                table: "tblProducts",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProviders_UserID",
                table: "tblProviders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserInformations_UserID",
                table: "tblUserInformations",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAdmins");

            migrationBuilder.DropTable(
                name: "tblCarts");

            migrationBuilder.DropTable(
                name: "tblProductImages");

            migrationBuilder.DropTable(
                name: "tblProductProviders");

            migrationBuilder.DropTable(
                name: "tblUserInformations");

            migrationBuilder.DropTable(
                name: "tblProducts");

            migrationBuilder.DropTable(
                name: "tblProviders");

            migrationBuilder.DropTable(
                name: "tblCategories");
        }
    }
}
