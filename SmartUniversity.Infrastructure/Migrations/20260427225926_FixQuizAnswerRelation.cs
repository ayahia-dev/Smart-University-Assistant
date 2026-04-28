using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartUniversity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixQuizAnswerRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_QuizAnswers_SubmissionId",
                table: "QuizAnswers",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAnswers_QuizSubmissions_SubmissionId",
                table: "QuizAnswers",
                column: "SubmissionId",
                principalTable: "QuizSubmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAnswers_QuizSubmissions_SubmissionId",
                table: "QuizAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuizAnswers_SubmissionId",
                table: "QuizAnswers");
        }
    }
}
