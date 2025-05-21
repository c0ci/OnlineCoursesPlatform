using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursesPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedbackAndGradeToSubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Grade",
                table: "Submissions",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Submissions");
        }
    }
}
