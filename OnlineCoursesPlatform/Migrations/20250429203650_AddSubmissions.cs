using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursesPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubmittedOn",
                table: "Submissions",
                newName: "SubmittedAt");

            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "Submissions",
                newName: "Content");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Submissions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_LectureId",
                table: "Submissions",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AspNetUsers_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Lectures_LectureId",
                table: "Submissions",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AspNetUsers_StudentId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Lectures_LectureId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_LectureId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "Submissions",
                newName: "SubmittedOn");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Submissions",
                newName: "FileUrl");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Submissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
