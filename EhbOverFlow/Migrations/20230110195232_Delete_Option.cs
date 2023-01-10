using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EhbOverFlow.Migrations
{
    public partial class Delete_Option : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_Categories_CategoryId",
                table: "notes");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_Categories_CategoryId",
                table: "notes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_Categories_CategoryId",
                table: "notes");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_Categories_CategoryId",
                table: "notes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
