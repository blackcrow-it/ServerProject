using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerProject.Migrations
{
    public partial class Ap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentGradeGradeId",
                table: "Credentials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentGradeRollNumber",
                table: "Credentials",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_StudentGradeRollNumber_StudentGradeGradeId",
                table: "Credentials",
                columns: new[] { "StudentGradeRollNumber", "StudentGradeGradeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_StudentGrade_StudentGradeRollNumber_StudentGradeGradeId",
                table: "Credentials",
                columns: new[] { "StudentGradeRollNumber", "StudentGradeGradeId" },
                principalTable: "StudentGrade",
                principalColumns: new[] { "RollNumber", "GradeId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_StudentGrade_StudentGradeRollNumber_StudentGradeGradeId",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_StudentGradeRollNumber_StudentGradeGradeId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "StudentGradeGradeId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "StudentGradeRollNumber",
                table: "Credentials");
        }
    }
}
