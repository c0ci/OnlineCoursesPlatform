using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCoursesPlatform.Migrations
{
    /// <inheritdoc />
    public partial class LecturerIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LecturerId",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6,
                column: "LecturerId",
                value: "userId");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7,
                column: "LecturerId",
                value: "userId");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8,
                column: "LecturerId",
                value: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6,
                column: "LecturerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7,
                column: "LecturerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8,
                column: "LecturerId",
                value: 1);
        }
    }
}
