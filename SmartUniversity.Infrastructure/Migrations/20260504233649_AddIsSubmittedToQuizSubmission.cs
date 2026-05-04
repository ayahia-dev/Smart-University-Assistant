using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUniversity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsSubmittedToQuizSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "QuizSubmissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "QuizSubmissions");
        }
    }
}
