using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUniversity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixStudentGradeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGrades_Users_UserId",
                table: "StudentGrades");

            migrationBuilder.DropIndex(
                name: "IX_StudentGrades_UserId",
                table: "StudentGrades");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentGrades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StudentGrades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrades_UserId",
                table: "StudentGrades",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGrades_Users_UserId",
                table: "StudentGrades",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
