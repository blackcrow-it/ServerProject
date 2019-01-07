using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerProject.Migrations
{
    public partial class addModelMark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avarta",
                table: "Marks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avarta",
                table: "Marks");
        }
    }
}
