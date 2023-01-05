using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EhbOverFlow.Migrations
{
    public partial class Added_Solved_TO_Note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Solved",
                table: "notes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Solved",
                table: "notes");
        }
    }
}
