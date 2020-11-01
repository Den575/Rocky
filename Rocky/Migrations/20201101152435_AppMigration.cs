using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocky.Migrations
{
    public partial class AppMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationType_Category_CategoryId",
                table: "ApplicationType");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationType_CategoryId",
                table: "ApplicationType");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ApplicationType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ApplicationType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationType_CategoryId",
                table: "ApplicationType",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationType_Category_CategoryId",
                table: "ApplicationType",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
