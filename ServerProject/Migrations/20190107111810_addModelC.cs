using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerProject.Migrations
{
    public partial class addModelC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avarta",
                table: "Marks");

            migrationBuilder.AddColumn<string>(
                name: "Avarta",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avarta",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Avarta",
                table: "Marks",
                nullable: true);
        }
    }
}
